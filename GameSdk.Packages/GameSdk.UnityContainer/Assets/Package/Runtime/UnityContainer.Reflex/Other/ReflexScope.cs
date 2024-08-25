using UnityEngine;
using Reflex.Core;
using GameSdk.Core.Loggers;
using System.Collections.Generic;
using System.Linq;

namespace GameSdk.UnityContainer.Reflex
{
    public class ReflexScope : MonoBehaviour, IInstaller
    {
        [SerializeField] private List<UnityInstaller> _unityInstallers;

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            var container = new ReflexUnityContainer(containerBuilder);

            _unityInstallers.ForEach(i => i.InstallBindings(container));

            containerBuilder.OnContainerBuilt += OnContainerBuilt;
        }

        private void OnContainerBuilt(Container container)
        {
            // Reflex has no scope nesting, so all kernel bindings are not duplicated
            var bootstraps = container.All<IBootstrap>();
            var nonLazies = container.All<INonLazy>();
            var initializables = container.All<IInitializable>();

            foreach (var bootstrap in bootstraps)
            {
                bootstrap.Boot();
            }

            SystemLog.Log(LogType.Log, "UnityContainer", $"NonLazy: {nonLazies.Count()}");

            foreach (var initializable in initializables)
            {
                initializable.Initialize();
            }
        }
    }
}
