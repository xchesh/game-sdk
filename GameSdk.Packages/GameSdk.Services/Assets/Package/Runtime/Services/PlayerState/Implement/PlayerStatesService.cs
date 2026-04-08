using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

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

        public async Awaitable Initialize()
        {
            var initializeTasks = _playerStateProviders
                .Select(static provider => AsTask(provider.Initialize()))
                .ToArray();

            await Task.WhenAll(initializeTasks);

            foreach (var playerState in _playerStates)
            {
                playerState.PropertyChanged += OnStateChanged;
            }
        }

        public Awaitable Load(PlayerStateProviderType type)
        {
            return LoadProvider(type);
        }

        public Awaitable Preload(PlayerStateProviderType type)
        {
            return PreloadProvider(type);
        }

        public Awaitable Save()
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

        private async Awaitable LoadProvider(PlayerStateProviderType type)
        {
            if (_playerStateProvidersByType.TryGetValue(type, out var provider))
            {
                await provider.Load(_playerStates);
            }
        }

        private async Awaitable PreloadProvider(PlayerStateProviderType type)
        {
            if (_playerStateProvidersByType.TryGetValue(type, out var provider))
            {
                await provider.Preload(_playerStates);
            }
        }

        private async Awaitable SaveDirty()
        {
            var tasks = new List<Task>();

            foreach (var playerStatesProvider in _playerStateProviders)
            {
                if (playerStatesProvider.IsEnabled)
                {
                    tasks.Add(AsTask(playerStatesProvider.Save(_dirtyPlayerStates)));
                }
            }

            if (tasks.Count > 0)
            {
                await Task.WhenAll(tasks);
            }

            _dirtyPlayerStates.Clear();
        }

        private void OnStateChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is IPlayerState state && _playerStatesByKeys.ContainsKey(state.Key))
            {
                _dirtyPlayerStates.Add(state);
            }
        }

        private static async Task AsTask(Awaitable awaitable)
        {
            await awaitable;
        }
    }
}
