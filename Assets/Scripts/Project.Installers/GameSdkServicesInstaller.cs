using Core.Common.UnityContainer;
using GameSdk.Services.InApp;

namespace Project.Installers
{
    public class GameSdkServicesInstaller : IUnityInstaller
    {
        public override void InstallBindings(IUnityContainer container)
        {
            container.Register<InAppService>().As<IInAppService>();
        }
    }
}