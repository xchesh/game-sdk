using Cysharp.Threading.Tasks;

namespace GameSdk.Services.RemoteConfig
{
    public interface IRemoteConfigService
    {
        IRemoteConfig AppConfig { get; }

        UniTask Initialize(params string[] configs);
    }
}