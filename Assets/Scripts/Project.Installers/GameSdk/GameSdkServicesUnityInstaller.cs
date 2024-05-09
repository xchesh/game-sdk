using Core.Common.UnityContainer;
using GameSdk.Services.RemoteConfig;
using GameSdk.Services.Unity;

namespace Project.Installers
{
    public class GameSdkServicesUnityInstaller : IUnityInstaller
    {
        public override void InstallBindings(IUnityContainer container)
        {
            container.Register<UnityRemoteConfigProvider>().As<IRemoteConfigProvider>();
        }
    }
}