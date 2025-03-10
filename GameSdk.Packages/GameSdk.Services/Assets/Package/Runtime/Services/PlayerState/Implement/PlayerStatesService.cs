using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace GameSdk.Services.PlayerState
{
    public class PlayerStatesService : IPlayerStatesService
    {
        private readonly IEnumerable<IPlayerState> _playerStates;
        private readonly IEnumerable<IPlayerStateProvider> _playerStateProviders;

        private readonly IList<IPlayerState> _dirtyPlayerStates = new List<IPlayerState>();

        private readonly IDictionary<string, IPlayerState> _playerStatesByKeys = new Dictionary<string, IPlayerState>();
        private readonly IDictionary<PlayerStateProviderType, IPlayerStateProvider> _playerStateProvidersByType = new Dictionary<PlayerStateProviderType, IPlayerStateProvider>();

        public PlayerStatesService(IEnumerable<IPlayerStateProvider> playerStateProviders, IEnumerable<IPlayerState> playerStates)
        {
            _playerStateProviders = playerStateProviders.OrderBy(provider => provider.Type);
            _playerStates = playerStates.OrderBy(state => state.Key);

            foreach (var playerStatesProvider in _playerStateProviders)
            {
                _playerStateProvidersByType.TryAdd(playerStatesProvider.Type, playerStatesProvider);
            }

            foreach (var playerState in _playerStates)
            {
                _playerStatesByKeys.TryAdd(playerState.Key, playerState);
            }
        }

        public async UniTask Initialize()
        {
            await UniTask.WhenAll(_playerStateProviders.Select(static provider => provider.Initialize()));

            foreach (var playerState in _playerStates)
            {
                playerState.PropertyChanged += OnStateChanged;
            }
        }

        public UniTask Load(PlayerStateProviderType type)
        {
            return LoadProvider(type);
        }

        public UniTask Preload(PlayerStateProviderType type)
        {
            return PreloadProvider(type);
        }

        public UniTask Save()
        {
            return SaveDirty();
        }

        public void SetProviderEnable(PlayerStateProviderType type, bool isEnable)
        {
            if (_playerStateProvidersByType.TryGetValue(type, out var provider))
            {
                provider.SetEnable(isEnable);
            }
        }

        private UniTask LoadProvider(PlayerStateProviderType type)
        {
            if (_playerStateProvidersByType.TryGetValue(type, out var provider))
            {
                return provider.Load(_playerStates);
            }

            return UniTask.CompletedTask;
        }

        private UniTask PreloadProvider(PlayerStateProviderType type)
        {
            if (_playerStateProvidersByType.TryGetValue(type, out var provider))
            {
                return provider.Preload(_playerStates);
            }

            return UniTask.CompletedTask;
        }

        private async UniTask SaveDirty()
        {
            var tasks = new List<UniTask>();

            foreach (var playerStatesProvider in _playerStateProviders)
            {
                if (playerStatesProvider.IsEnabled)
                {
                    tasks.Add(playerStatesProvider.Save(_dirtyPlayerStates));
                }
            }

            await UniTask.WhenAll(tasks);

            _dirtyPlayerStates.Clear();
        }

        private void OnStateChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is IPlayerState state && _playerStatesByKeys.ContainsKey(state.Key))
            {
                _dirtyPlayerStates.Add(state);
            }
        }
    }
}
