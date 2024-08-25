using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameSdk.UnityContainer.VContainer
{
    public abstract class VContainerScope : LifetimeScope
    {
        [Header("Unity Installers")]
        [SerializeField] private List<UnityInstaller> _unityInstallers;

        protected override void Configure(IContainerBuilder builder)
        {
            var container = new VContainerUnityContainer(builder);

            _unityInstallers.ForEach(i => i.InstallBindings(container));

            builder.RegisterEntryPoint<VContainerKernel>().AsSelf();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _unityInstallers.Clear();
        }
    }
}
