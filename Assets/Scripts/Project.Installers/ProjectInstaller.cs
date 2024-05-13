using GameSdk.Services.RemoteConfig;
using Project.Common.UnityContainer;
using Project.Services.RemoteConfig;

namespace Project.Installers
{
    public class ProjectInstaller : IUnityInstaller
    {
        public override void InstallBindings(IUnityContainer container)
        {
            // Don't use the VContainer.Unity.IInitializable directly, use the UnityContainer.IInitializable instead
            container.Register<Bootstrap>().As<VContainer.Unity.IInitializable>();

            container.Register<ProjectRemoteConfigAttribution>().As<IRemoteConfigAttribution>();
        }
    }
}