using Core.Common.UnityContainer;
using GameSdk.Services.RemoteConfig;
using Project.Services.RemoteConfig;

namespace Project.Installers
{
    public class ProjectInstaller : IUnityInstaller
    {
        public override void InstallBindings(IUnityContainer container)
        {
            container.Register<ProjectRemoteConfigAttribution>().As<IRemoteConfigAttribution>();
        }
    }
}