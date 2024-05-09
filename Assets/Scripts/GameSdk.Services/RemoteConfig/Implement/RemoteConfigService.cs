using Cysharp.Threading.Tasks;
using UnityEngine.Assertions;

namespace GameSdk.Services.RemoteConfig
{
    public class RemoteConfigService : IRemoteConfigService
    {
        private readonly IRemoteConfigProvider _remoteConfigProvider;
        private readonly IRemoteConfigAttribution _attribution;

        public IRemoteConfig AppConfig => _remoteConfigProvider?.AppConfig;

        public RemoteConfigService(IRemoteConfigProvider remoteConfigProvider, IRemoteConfigAttribution attribution)
        {
            _remoteConfigProvider = remoteConfigProvider;
            _attribution = attribution;
        }

        public UniTask Initialize(params string[] configs)
        {
            Assert.IsNotNull(_remoteConfigProvider, "RemoteConfig Provider is null");
            Assert.IsNotNull(_attribution, "RemoteConfig Attribution is null");

            var (user, app, filter) = _attribution.GetAttributes();

            if (configs.Length == 0)
            {
                return _remoteConfigProvider.FetchConfig(user, app, filter);
            }

            var tasks = new UniTask[configs.Length];

            for (var i = 0; i < configs.Length; i++)
            {
                tasks[i] = _remoteConfigProvider.FetchConfig(configs[i], user, app, filter);
            }

            return UniTask.WhenAll(tasks);
        }
    }
}