using GameSdk.Services.Unity;
using GameSdk.Services.Authentication;
using GameSdk.Services.RemoteConfig;

using GameSdk.UnityContainer;

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
