using UnityEngine;
using Reflex.Core;
using GameSdk.Core.Loggers;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Project.Common.UnityContainer.Reflex
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
            var systemLogger = container.Resolve<ISystemLogger>();
            var bootstrap = container.Resolve<IBootstrap>();
            var nonLazies = container.All<INonLazy>();
            var initializables = container.All<IInitializable>();

            bootstrap.Boot();

            systemLogger.Log(LogType.Log, "UnityContainer", $"NonLazy: {nonLazies.Count()}");

            foreach (var initializable in initializables)
            {
                initializable.Initialize();
            }
        }
    }
}
