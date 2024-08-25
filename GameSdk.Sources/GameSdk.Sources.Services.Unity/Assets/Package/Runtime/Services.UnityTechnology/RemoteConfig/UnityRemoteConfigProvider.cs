using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameSdk.Sources.Services.RemoteConfig;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using RemoteConfigService = Unity.Services.RemoteConfig.RemoteConfigService;

namespace GameSdk.Sources.Services.UnityTechnology
{
    public class UnityRemoteConfigProvider : IRemoteConfigProvider
    {
        private static RemoteConfigService UnityRemoteConfig => RemoteConfigService.Instance;

        private readonly Dictionary<string, IRemoteConfig> _configs = new();

        public event Action<IRemoteConfig> ConfigFetched;
        public event Action<string, string> ConfigFetchFailed;

        public string AppConfigType { get; private set; }
        public string AppConfigVersion { get; private set; }
        public IRemoteConfig AppConfig { get; private set; }

        public UniTask Initialize()
        {
            UnityRemoteConfig.FetchCompleted += ApplyRemoteConfig;

            return UnityServicesUtility.Initialize();
        }

        public IRemoteConfig GetConfig(string configType)
        {
            if (_configs.ContainsKey(configType) is false)
            {
                var runtimeConfig = UnityRemoteConfig.GetConfig(configType);
                var config = new UnityRemoteConfig(runtimeConfig);

                _configs.Add(configType, config);
            }

            return _configs[configType];
        }

        public async UniTask FetchConfig<T1, T2, T3>(string configType, T1 userAttributes, T2 appAttributes, T3 filterAttributes)
            where T1 : IUserAttributes
            where T2 : IAppAttributes
            where T3 : IFilterAttributes
        {
            try
            {
                if (await UnityServicesUtility.Initialize() != ServicesInitializationState.Initialized)
                {
                    throw new RequestFailedException(
                        CommonErrorCodes.ServiceUnavailable,
                        "Unity Services not initialized"
                    );
                }

                await UnityRemoteConfig.FetchConfigsAsync(configType, userAttributes, appAttributes, filterAttributes);

                ConfigFetched?.Invoke(GetConfig(configType));
            }
            catch (Exception e)
            {
                ConfigFetchFailed?.Invoke(configType, e.Message);

                throw new Exception("Failed to fetch remote config", e);
            }
        }

        public UniTask FetchConfig<T1, T2, T3>(T1 userAttributes, T2 appAttributes, T3 filterAttributes)
            where T1 : IUserAttributes
            where T2 : IAppAttributes
            where T3 : IFilterAttributes
        {
            var appConfigType = UnityRemoteConfig.appConfig.configType;

            return FetchConfig(appConfigType, userAttributes, appAttributes, filterAttributes);
        }

        private void ApplyRemoteConfig(ConfigResponse response)
        {
            AppConfigType = UnityRemoteConfig.appConfig.configType;
            AppConfigVersion = UnityRemoteConfig.appConfig.environmentId;

            AppConfig = new UnityRemoteConfig(UnityRemoteConfig.appConfig);
        }
    }
}
