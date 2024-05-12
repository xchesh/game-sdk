using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Project.Common.UnityContainer.VContainer
{
    public abstract class VContainerScope : LifetimeScope
    {
        [Header("UnityContainer Scope")] [SerializeField, Space(5)]
        private bool _autoInjectScene;

        [SerializeField] private List<UnityInstaller> _unityInstallers;

        public bool AutoInjectScene => _autoInjectScene;

        protected override void Awake()
        {
            if (_autoInjectScene)
            {
                autoInjectGameObjects.Clear();
                autoInjectGameObjects.AddRange(gameObject.scene.GetRootGameObjects());
            }

            base.Awake();
        }

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
