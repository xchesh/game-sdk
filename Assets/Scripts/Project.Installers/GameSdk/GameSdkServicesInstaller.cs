using Core.Common.UnityContainer;
using GameSdk.Services.InApp;
using GameSdk.Services.InternetReachability;
using GameSdk.Services.RemoteConfig;
using UnityEngine;

namespace Project.Installers
{
    public class GameSdkServicesInstaller : IUnityInstaller
    {
        [SerializeField] private InternetReachabilityConfig _internetReachabilityConfig;

        public override void InstallBindings(IUnityContainer container)
        {
            // Internet Reachability
            UnityEngine.Assertions.Assert.IsNotNull(_internetReachabilityConfig);
            container.RegisterInstance(_internetReachabilityConfig).As<InternetReachabilityConfig>();
            container.Register<InternetReachabilityService>().As<IInternetReachabilityService>();

            // In-App
            container.Register<InAppService>().As<IInAppService>();

            // Remote Config
            container.Register<RemoteConfigContext>().As<IRemoteConfigContext>();
            container.Register<RemoteConfigService>().As<IRemoteConfigService>();
        }
    }
}