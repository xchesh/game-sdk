using Cysharp.Threading.Tasks;

namespace GameSdk.Sources.Services.RemoteConfig
{
    public interface IRemoteConfigService : IRemoteConfigProvider
    {
        UniTask Initialize(params string[] configs);

        internal void RegisterListener(IRemoteConfigListener listener);
        internal void UnregisterListener(IRemoteConfigListener listener);
    }
}
