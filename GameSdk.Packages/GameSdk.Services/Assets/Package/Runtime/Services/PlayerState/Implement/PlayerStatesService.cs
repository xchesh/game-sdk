using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace GameSdk.Services.PlayerState
{
    public class PlayerStatesService : IPlayerStatesService
    {
        private readonly IEnumerable<IPlayerState> _playerStates;
        private readonly IEnumerable<IPlayerStatesProvider> _playerStatesProviders;

        private readonly IList<IPlayerState> _dirtyPlayerStates = new List<IPlayerState>();

        private readonly IDictionary<string, IPlayerState> _playerStatesByKeys = new Dictionary<string, IPlayerState>();
        private readonly IDictionary<PlayerStateProviderType, IPlayerStatesProvider> _playerStatesProvidersByType = new Dictionary<PlayerStateProviderType, IPlayerStatesProvider>();

        public PlayerStatesService(IEnumerable<IPlayerStatesProvider> playerStatesProviders, IEnumerable<IPlayerState> playerStates)
        {
            _playerStatesProviders = playerStatesProviders.OrderBy(provider => provider.Type);
            _playerStates = playerStates.OrderBy(state => state.Key);

            foreach (var playerStatesProvider in _playerStatesProviders)
            {
                _playerStatesProvidersByType.TryAdd(playerStatesProvider.Type, playerStatesProvider);
            }

            foreach (var playerState in _playerStates)
            {
                _playerStatesByKeys.TryAdd(playerState.Key, playerState);
            }
        }

        public async UniTask Initialize()
        {
            await UniTask.WhenAll(_playerStatesProviders.Select(static provider => provider.Initialize()));

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
            if (_playerStatesProvidersByType.TryGetValue(type, out var provider))
            {
                provider.SetEnable(isEnable);
            }
        }

        private UniTask LoadProvider(PlayerStateProviderType type)
        {
            if (_playerStatesProvidersByType.TryGetValue(type, out var provider))
            {
                return provider.Load(_playerStates);
            }

            return UniTask.CompletedTask;
        }

        private UniTask PreloadProvider(PlayerStateProviderType type)
        {
            if (_playerStatesProvidersByType.TryGetValue(type, out var provider))
            {
                return provider.Preload(_playerStates);
            }

            return UniTask.CompletedTask;
        }

        private async UniTask SaveDirty()
        {
            var tasks = new List<UniTask>();

            foreach (var playerStatesProvider in _playerStatesProviders)
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
