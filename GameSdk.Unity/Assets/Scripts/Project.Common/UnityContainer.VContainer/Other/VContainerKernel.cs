using System.Collections.Generic;
using System.Linq;
using GameSdk.Sources.Core.Loggers;
using UnityEngine;
using VContainer.Internal;
using VContainer.Unity;

namespace Project.Common.UnityContainer.VContainer
{
    public class VContainerKernel : IPostInitializable
    {
        private readonly IEnumerable<IBootstrap> _bootstraps;
        private readonly IEnumerable<IInitializable> _initializables;
        private readonly IEnumerable<INonLazy> _nonLazies;

        public VContainerKernel(
            ContainerLocal<IReadOnlyList<IBootstrap>> localBootstraps,
            ContainerLocal<IReadOnlyList<INonLazy>> localNonLazies,
            ContainerLocal<IReadOnlyList<IInitializable>> localInitializables)
        {
            _bootstraps = localBootstraps.Value;
            _nonLazies = localNonLazies.Value;
            _initializables = localInitializables.Value;
        }

        public void PostInitialize()
        {
            foreach (var bootstrap in _bootstraps)
            {
                bootstrap.Boot();
            }

            SystemLog.Log(LogType.Log, "UnityContainer", $"NonLazy: {_nonLazies.Count()}");

            foreach (var unityInitializable in _initializables.OrderBy(i => i.Order))
            {
                unityInitializable.Initialize();
            }
        }
    }
}
