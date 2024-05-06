using Cysharp.Threading.Tasks;

namespace GameSdk.Services.RemoteConfig
{
    public interface IRemoteConfigProvider
    {
        string Version { get; }

        IRemoteConfig Config { get; }

        UniTask FetchConfigs();
    }
}