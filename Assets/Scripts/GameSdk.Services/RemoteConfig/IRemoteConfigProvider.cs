using System;
using Cysharp.Threading.Tasks;

namespace GameSdk.Services.RemoteConfig
{
    public interface IRemoteConfigProvider
    {
        event Action<IRemoteConfig> ConfigFetched;
        event Action<string, string> ConfigFetchFailed;

        string AppConfigType { get; }
        string AppConfigVersion { get; }

        IRemoteConfig AppConfig { get; }

        UniTask Initialize();

        IRemoteConfig GetConfig(string configType);

        UniTask FetchConfig<T1, T2, T3>(string configType, T1 userAttributes, T2 appAttributes, T3 filterAttributes)
            where T1 : IUserAttributes
            where T2 : IAppAttributes
            where T3 : IFilterAttributes;

        UniTask FetchConfig<T1, T2, T3>(T1 userAttributes, T2 appAttributes, T3 filterAttributes)
            where T1 : IUserAttributes
            where T2 : IAppAttributes
            where T3 : IFilterAttributes;
    }
}