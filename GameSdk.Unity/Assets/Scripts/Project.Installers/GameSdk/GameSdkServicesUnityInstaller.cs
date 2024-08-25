using GameSdk.Sources.Services.UnityTechnology;
using GameSdk.Sources.Services.Authentication;
using GameSdk.Sources.Services.RemoteConfig;

using Project.Common.UnityContainer;

namespace Project.Installers
{
    public class GameSdkServicesUnityInstaller : IUnityInstaller
    {
        public override void InstallBindings(IUnityContainer container)
        {
            container.Register<UnityAuthenticationProvider>().As<IAuthenticationProvider>();
            container.Register<UnityRemoteConfigProvider>().As<IRemoteConfigProvider>();
        }
    }
}
