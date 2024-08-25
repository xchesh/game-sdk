using System.Collections.Generic;

namespace GameSdk.Sources.Services.RemoteConfig
{
    [JetBrains.Annotations.UsedImplicitly]
    public class RemoteConfigContext : IRemoteConfigContext
    {
        private readonly IRemoteConfigService _remoteConfigService;
        private readonly IEnumerable<IRemoteConfigListener> _listeners;

        [UnityEngine.Scripting.RequiredMember]
        public RemoteConfigContext(
            IRemoteConfigService remoteConfigService,
            IEnumerable<IRemoteConfigListener> listeners)
        {
            _remoteConfigService = remoteConfigService;

            foreach (var listener in listeners)
            {
                _remoteConfigService.RegisterListener(listener);
            }
        }

        public void Dispose()
        {
            foreach (var listener in _listeners)
            {
                _remoteConfigService.UnregisterListener(listener);
            }
        }
    }
}
