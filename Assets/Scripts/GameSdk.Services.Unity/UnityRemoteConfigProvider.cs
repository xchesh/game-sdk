using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameSdk.Services.RemoteConfig;
using Unity.Services.RemoteConfig;
using UnityEngine;
using RemoteConfigService = Unity.Services.RemoteConfig.RemoteConfigService;

namespace GameSdk.Services.Unity
{
    public class UnityRemoteConfigProvider : IRemoteConfigProvider
    {
        private readonly Dictionary<string, IRemoteConfig> _configs = new();

        public string AppConfigType { get; private set; }
        public string AppConfigVersion { get; private set; }
        public IRemoteConfig AppConfig { get; private set; }

        public IRemoteConfig GetConfig(string configType)
        {
            if (_configs.ContainsKey(configType) is false)
            {
                var runtimeConfig = RemoteConfigService.Instance.GetConfig(configType);
                var config = new UnityRemoteConfig(runtimeConfig);

                _configs.Add(configType, config);
            }

            return _configs[configType];
        }

        public async UniTask FetchConfig<T1, T2, T3>(string configType, T1 userAttributes, T2 appAttributes,
            T3 filterAttributes)
            where T1 : IUserAttributes
            where T2 : IAppAttributes
            where T3 : IFilterAttributes
        {
            try
            {
                RemoteConfigService.Instance.FetchCompleted += ApplyRemoteConfig;
                await RemoteConfigService.Instance.FetchConfigsAsync(configType, userAttributes, appAttributes, filterAttributes);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to fetch remote config", e);
            }
        }

        public UniTask FetchConfig<T1, T2, T3>(T1 userAttributes, T2 appAttributes, T3 filterAttributes)
            where T1 : IUserAttributes
            where T2 : IAppAttributes
            where T3 : IFilterAttributes
        {
            var appConfigType = RemoteConfigService.Instance.appConfig.configType;

            return FetchConfig(appConfigType, userAttributes, appAttributes, filterAttributes);
        }

        private void ApplyRemoteConfig(ConfigResponse response)
        {
            AppConfigType = RemoteConfigService.Instance.appConfig.configType;
            AppConfigVersion = RemoteConfigService.Instance.appConfig.environmentId;

            AppConfig = new UnityRemoteConfig(RemoteConfigService.Instance.appConfig);
        }
    }
}