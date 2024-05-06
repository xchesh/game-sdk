using System.Collections.Generic;
using System.Linq;
using GameSdk.Core.Loggers;
using UnityEngine;
using VContainer.Internal;
using VContainer.Unity;

namespace Core.Common.UnityContainer
{
    public class VContainerKernel : IStartable
    {
        private readonly IEnumerable<IInitializable> _initializables;
        private readonly IEnumerable<INonLazy> _nonLazies;
        private readonly ISystemLogger _logger;

        public VContainerKernel(
            ContainerLocal<IReadOnlyList<IInitializable>> localInitializables,
            ContainerLocal<IReadOnlyList<INonLazy>> localNonLazies,
            ISystemLogger logger)
        {
            _initializables = localInitializables.Value;
            _nonLazies = localNonLazies.Value;
            _logger = logger;
        }

        public void Start()
        {
            _logger.Log(LogType.Log, "UnityContainer.VContainer", $"NonLazy: {_nonLazies.Count()}");

            foreach (var unityInitializable in _initializables.OrderBy(i => i.Order))
            {
                unityInitializable.Initialize();
            }
        }
    }
}