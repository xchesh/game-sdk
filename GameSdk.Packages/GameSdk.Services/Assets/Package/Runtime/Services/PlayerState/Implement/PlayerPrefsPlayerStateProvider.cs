using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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

        public UniTask Initialize()
        {
            return UniTask.CompletedTask;
        }

        public UniTask Save(IEnumerable<IPlayerState> states)
        {
            foreach (var state in states)
            {
                var prefKey = GetKey(state.Key);

                UnityEngine.PlayerPrefs.SetString(prefKey, state.ToJson());
            }

            return UniTask.CompletedTask;
        }

        public UniTask Load(IEnumerable<IPlayerState> states)
        {
            foreach (var state in states)
            {
                var prefKey = GetKey(state.Key);

                if (UnityEngine.PlayerPrefs.HasKey(prefKey))
                {
                    state.FromJson(UnityEngine.PlayerPrefs.GetString(prefKey));
                }
            }

            return UniTask.CompletedTask;
        }

        public UniTask Preload(IEnumerable<IPlayerState> states)
        {
            return UniTask.CompletedTask;
        }

        public UniTask Delete(params string[] keys)
        {
            foreach (var key in keys)
            {
                var prefKey = GetKey(key);

                if (UnityEngine.PlayerPrefs.HasKey(prefKey))
                {
                    UnityEngine.PlayerPrefs.DeleteKey(prefKey);
                }
            }

            return UniTask.CompletedTask;
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
