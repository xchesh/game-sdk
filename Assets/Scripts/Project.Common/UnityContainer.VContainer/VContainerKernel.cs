using System.Collections.Generic;
using System.Linq;
using GameSdk.Core.Loggers;
using UnityEngine;
using VContainer.Internal;
using VContainer.Unity;

namespace Project.Common.UnityContainer.VContainer
{
    public class VContainerKernel : IPostInitializable
    {
        private readonly IEnumerable<IInitializable> _initializables;
        private readonly IEnumerable<INonLazy> _nonLazies;
        private readonly ISystemLogger _systemLogger;

        public VContainerKernel(
            ContainerLocal<IReadOnlyList<IInitializable>> localInitializables,
            ContainerLocal<IReadOnlyList<INonLazy>> localNonLazies,
            ISystemLogger systemLogger)
        {
            _initializables = localInitializables.Value;
            _nonLazies = localNonLazies.Value;
            _systemLogger = systemLogger;
        }

        public void PostInitialize()
        {
            _systemLogger.Log(LogType.Log, "UnityContainer", $"NonLazy: {_nonLazies.Count()}");

            foreach (var unityInitializable in _initializables.OrderBy(i => i.Order))
            {
                unityInitializable.Initialize();
            }
        }
    }
}