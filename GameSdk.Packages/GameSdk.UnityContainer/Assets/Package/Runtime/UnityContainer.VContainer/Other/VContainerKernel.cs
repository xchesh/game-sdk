using System.Collections.Generic;
using System.Linq;
using VContainer.Internal;
using VContainer.Unity;

namespace GameSdk.UnityContainer.VContainer
{
    public class VContainerKernel : IPostInitializable
    {
        private readonly IEnumerable<IBootstrap> _bootstraps;
        private readonly IEnumerable<IInitializable> _initializables;

        public VContainerKernel(
            ContainerLocal<IReadOnlyList<IBootstrap>> localBootstraps,
            ContainerLocal<IReadOnlyList<IInitializable>> localInitializables)
        {
            _bootstraps = localBootstraps.Value;
            _initializables = localInitializables.Value;
        }

        public void PostInitialize()
        {
            foreach (var bootstrap in _bootstraps)
            {
                bootstrap.Boot();
            }

            foreach (var unityInitializable in _initializables.OrderBy(i => i.Order))
            {
                unityInitializable.Initialize();
            }
        }
    }
}
