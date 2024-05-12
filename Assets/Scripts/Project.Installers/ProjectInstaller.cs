using GameSdk.Services.RemoteConfig;
using Project.Common.UnityContainer;
using Project.Services.RemoteConfig;

namespace Project.Installers
{
    public class ProjectInstaller : IUnityInstaller
    {
        public override void InstallBindings(IUnityContainer container)
        {
            container.Register<Bootstrap>().As<Bootstrap>();

            container.Register<ProjectRemoteConfigAttribution>().As<IRemoteConfigAttribution>();
        }
    }
}