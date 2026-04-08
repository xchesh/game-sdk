using UnityEngine;

namespace GameSdk.Services.RemoteConfig
{
    public interface IRemoteConfigService : IRemoteConfigProvider
    {
        Awaitable Initialize(params string[] configs);

        internal void RegisterListener(IRemoteConfigListener listener);
        internal void UnregisterListener(IRemoteConfigListener listener);
    }
}
