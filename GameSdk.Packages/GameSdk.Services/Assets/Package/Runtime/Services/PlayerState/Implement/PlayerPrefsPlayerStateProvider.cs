using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

namespace GameSdk.Services.PlayerState
{
    public class PlayerPrefsPlayerStateProvider : IPlayerStateProvider
    {
        private const string PREFIX = "PlayerState.";

        public PlayerStateProviderType Type { get; }
        public bool IsEnabled { get; private set; }

        [RequiredMember]
        public PlayerPrefsPlayerStateProvider(PlayerStateProviderType type, bool isEnabled)
        {
            Type = type;
            IsEnabled = isEnabled;
        }

        public async Awaitable Initialize()
        {
            await Awaitable.MainThreadAsync();
        }

        public async Awaitable Save(IEnumerable<IPlayerState> states)
        {
            await Awaitable.MainThreadAsync();

            foreach (var state in states)
            {
                var prefKey = GetKey(state.Key);
                PlayerPrefs.SetString(prefKey, state.ToJson());
            }
        }

        public async Awaitable Load(IEnumerable<IPlayerState> states)
        {
            await Awaitable.MainThreadAsync();

            foreach (var state in states)
            {
                var prefKey = GetKey(state.Key);

                if (PlayerPrefs.HasKey(prefKey))
                {
                    state.FromJson(PlayerPrefs.GetString(prefKey));
                }
            }
        }

        public async Awaitable Preload(IEnumerable<IPlayerState> states)
        {
            await Awaitable.MainThreadAsync();
        }

        public async Awaitable Delete(params string[] keys)
        {
            await Awaitable.MainThreadAsync();

            foreach (var key in keys)
            {
                var prefKey = GetKey(key);

                if (PlayerPrefs.HasKey(prefKey))
                {
                    PlayerPrefs.DeleteKey(prefKey);
                }
            }
        }

        public void SetEnable(bool isEnable)
        {
            IsEnabled = isEnable;
        }

        private string GetKey(string key)
        {
            return $"{PREFIX}{key}";
        }
    }
}
