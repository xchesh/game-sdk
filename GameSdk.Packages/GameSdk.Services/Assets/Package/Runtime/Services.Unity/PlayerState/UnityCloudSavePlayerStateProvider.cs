#if UNITY_SERVICES_CORE && UNITY_SERVICES_CLOUD_SAVE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameSdk.Core.Loggers;
using GameSdk.Services.PlayerState;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Scripting;
using DeleteOptions = Unity.Services.CloudSave.Models.Data.Player.DeleteOptions;

namespace GameSdk.Services.Unity
{
    public class UnityCloudSavePlayerStateProvider : IPlayerStateProvider
    {
        public PlayerStateProviderType Type { get; }
        public bool IsEnabled { get; private set; }

        private Dictionary<string, Item> _cachedData = new();

        [RequiredMember]
        public UnityCloudSavePlayerStateProvider(PlayerStateProviderType type, bool isEnabled)
        {
            Type = type;
            IsEnabled = isEnabled;
        }

        public async Awaitable Initialize()
        {
            while (UnityServices.State != ServicesInitializationState.Initialized)
            {
                await Awaitable.NextFrameAsync();
            }
        }

        public async Awaitable Save(IEnumerable<IPlayerState> states)
        {
            ClearCache();

            try
            {
                var dataToSave = new Dictionary<string, object>();

                foreach (var state in states)
                {
                    dataToSave.Add(state.Key, state.ToJson());
                }

                await CloudSaveService.Instance.Data.Player.SaveAsync(dataToSave);
            }
            catch (Exception e)
            {
                SystemLog.LogException(e);
            }
        }

        public async Awaitable Load(IEnumerable<IPlayerState> states)
        {
            var playerStates = states as IPlayerState[] ?? states.ToArray();

            if (_cachedData == null || _cachedData.Count == 0)
            {
                await Preload(playerStates);
            }

            foreach (var state in playerStates)
            {
                if (_cachedData != null && _cachedData.TryGetValue(state.Key, out var value))
                {
                    state.FromJson(value.ToString());
                }
            }
        }

        public async Awaitable Preload(IEnumerable<IPlayerState> states)
        {
            try
            {
                var keys = states.Select(state => state.Key).ToHashSet();

                _cachedData = await CloudSaveService.Instance.Data.Player.LoadAsync(keys);
            }
            catch (Exception e)
            {
                SystemLog.LogException(e);
            }
        }

        public async Awaitable Delete(params string[] keys)
        {
            ClearCache();

            try
            {
                var tasks = new List<Task>();

                foreach (var key in keys)
                {
                    tasks.Add(CloudSaveService.Instance.Data.Player.DeleteAsync(key, new DeleteOptions()));
                }

                await Task.WhenAll(tasks);
            }
            catch (Exception e)
            {
                SystemLog.LogException(e);
            }
        }


        public void SetEnable(bool isEnable)
        {
            IsEnabled = isEnable;
        }

        private void ClearCache()
        {
            _cachedData = null;
        }
    }
}
#endif
