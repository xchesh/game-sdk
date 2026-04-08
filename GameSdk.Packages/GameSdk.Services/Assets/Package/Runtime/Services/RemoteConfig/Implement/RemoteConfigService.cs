using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameSdk.Services.RemoteConfig
{
    [JetBrains.Annotations.UsedImplicitly]
    public class RemoteConfigService : IRemoteConfigService
    {
        private readonly Dictionary<IRemoteConfigListener, int> _listeners = new();
        private readonly IRemoteConfigProvider _remoteConfigProvider;
        private readonly IRemoteConfigAttribution _attribution;

        public event Action<IRemoteConfig> ConfigFetched;
        public event Action<string, string> ConfigFetchFailed;

        public string AppConfigType => _remoteConfigProvider.AppConfigType;
        public string AppConfigVersion => _remoteConfigProvider.AppConfigVersion;

        public IRemoteConfig AppConfig => _remoteConfigProvider.AppConfig;

        [UnityEngine.Scripting.RequiredMember]
        public RemoteConfigService(IRemoteConfigProvider remoteConfigProvider, IRemoteConfigAttribution attribution)
        {
            _remoteConfigProvider = remoteConfigProvider;
            _attribution = attribution;

            _remoteConfigProvider.ConfigFetched += OnConfigFetched;
            _remoteConfigProvider.ConfigFetchFailed += OnConfigFetchFailed;
        }

        public Awaitable Initialize()
        {
            return Initialize(AppConfigType);
        }

        public async Awaitable Initialize(params string[] configs)
        {
            Assert.IsNotNull(_remoteConfigProvider, "RemoteConfig Provider is null");
            Assert.IsNotNull(_attribution, "RemoteConfig Attribution is null");

            await _remoteConfigProvider.Initialize();

            var (user, app, filter) = _attribution.GetAttributes();

            if (configs.Length == 0)
            {
                await _remoteConfigProvider.FetchConfig(user, app, filter);

                return;
            }

            var tasks = new Task[configs.Length];

            for (var i = 0; i < configs.Length; i++)
            {
                tasks[i] = FetchConfigAsTask(configs[i], user, app, filter);
            }

            await Task.WhenAll(tasks);
        }

        public IRemoteConfig GetConfig(string configType)
        {
            return _remoteConfigProvider.GetConfig(configType);
        }

        public Awaitable FetchConfig<T1, T2, T3>(string configType, T1 userAttributes, T2 appAttributes, T3 filterAttributes)
            where T1 : IUserAttributes
            where T2 : IAppAttributes
            where T3 : IFilterAttributes
        {
            return _remoteConfigProvider.FetchConfig(configType, userAttributes, appAttributes, filterAttributes);
        }

        public Awaitable FetchConfig<T1, T2, T3>(T1 userAttributes, T2 appAttributes, T3 filterAttributes)
            where T1 : IUserAttributes
            where T2 : IAppAttributes
            where T3 : IFilterAttributes
        {
            return _remoteConfigProvider.FetchConfig(userAttributes, appAttributes, filterAttributes);
        }

        private async Task FetchConfigAsTask<T1, T2, T3>(string configType, T1 userAttributes, T2 appAttributes, T3 filterAttributes)
            where T1 : IUserAttributes
            where T2 : IAppAttributes
            where T3 : IFilterAttributes
        {
            await _remoteConfigProvider.FetchConfig(configType, userAttributes, appAttributes, filterAttributes);
        }

        private void OnConfigFetched(IRemoteConfig remoteConfig)
        {
            foreach (var listener in _listeners.Keys)
            {
                listener.OnConfigFetched(remoteConfig);
            }

            ConfigFetched?.Invoke(remoteConfig);
        }

        private void OnConfigFetchFailed(string configType, string error)
        {
            ConfigFetchFailed?.Invoke(configType, error);
        }

        void IRemoteConfigService.RegisterListener(IRemoteConfigListener listener)
        {
            _listeners.TryAdd(listener, 0);
            _listeners[listener] += 1;
        }

        void IRemoteConfigService.UnregisterListener(IRemoteConfigListener listener)
        {
            if (_listeners.ContainsKey(listener) is false)
            {
                return;
            }

            _listeners[listener] -= 1;

            if (_listeners[listener] < 1)
            {
                _listeners.Remove(listener);
            }
        }
    }
}
