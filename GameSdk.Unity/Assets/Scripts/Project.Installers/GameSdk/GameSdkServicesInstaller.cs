using GameSdk.Sources.Core.Conditions;
using GameSdk.Sources.Services.Authentication;
using GameSdk.Sources.Services.GraphicQuality;
using GameSdk.Sources.Services.InApp;
using GameSdk.Sources.Services.InternetReachability;
using GameSdk.Sources.Services.RemoteConfig;
using Project.Common.UnityContainer;
using UnityEngine;

namespace Project.Installers
{
    public class GameSdkServicesInstaller : IUnityInstaller
    {
        [SerializeField] private AuthenticationConfig _authenticationConfig;
        [SerializeField] private InternetReachabilityConfig _internetReachabilityConfig;
        [SerializeField] private GraphicQualityConfig _graphicQualityConfig;

        public override void InstallBindings(IUnityContainer container)
        {
            // Authentication
            UnityEngine.Assertions.Assert.IsNotNull(_authenticationConfig, "AuthenticationConfig is not set");
            container.RegisterInstance(_authenticationConfig).As<AuthenticationConfig>();
            container.Register<AuthenticationService>().As<IAuthenticationService>();

            // Internet Reachability
            UnityEngine.Assertions.Assert.IsNotNull(_internetReachabilityConfig, "InternetReachabilityConfig is not set");
            container.RegisterInstance(_internetReachabilityConfig).As<InternetReachabilityConfig>();
            container.Register<InternetReachabilityService>().As<IInternetReachabilityService>();

            // In-App
            container.Register<InAppService>().As<IInAppService>();

            // Remote Config
            container.Register<RemoteConfigContext>().As<IRemoteConfigContext>();
            container.Register<RemoteConfigService>().As<IRemoteConfigService>();

            // Graphic Quality
            UnityEngine.Assertions.Assert.IsNotNull(_graphicQualityConfig, "GraphicQualityConfig is not set");
            container.RegisterInstance(_graphicQualityConfig).As<IGraphicQualityConfig>();
            container.Register<GraphicQualityService>().As<IGraphicQualityService>();

            // Graphic Quality Conditions
            container.Register<DeviceIdCondition>().As<ICondition>();
            container.Register<DeviceModelsCondition>().As<ICondition>();
            container.Register<GraphicsDeviceNameCondition>().As<ICondition>();
            container.Register<GraphicsDeviceTypeCondition>().As<ICondition>();
            container.Register<MemorySizeCondition>().As<ICondition>();
        }
    }
}
