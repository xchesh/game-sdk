using Core.Common.UnityContainer;
using GameSdk.Services.InApp;

namespace Project.Installers
{
    public class SdkServicesInstaller : IUnityInstaller
    {
        public override void InstallBindings(IUnityContainer container)
        {
            container.Register<InAppService>().As<IInAppService>();
        }
    }
}