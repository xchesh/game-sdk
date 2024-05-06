using GameSdk.Core.Loggers;

namespace GameSdk.Services.RemoteConfig
{
    public class RemoteConfigService : IRemoteConfigService
    {
        private readonly IRemoteConfigProvider _remoteConfigProvider;

        public RemoteConfigService(IRemoteConfigProvider remoteConfigProvider)
        {
            _remoteConfigProvider = remoteConfigProvider;
        }

        public IRemoteConfig GetConfig()
        {
            if (_remoteConfigProvider == null)
            {
                SystemLog.LogException(new System.Exception("RemoteConfigProvider is null"));

                return null;
            }

            return _remoteConfigProvider.Config;
        }
    }
}