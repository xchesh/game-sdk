using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameSdk.UnityContainer.VContainer
{
    [UnityEngine.Scripting.Preserve]
    public class VContainerUnityContainerResolverMono : MonoBehaviour, IUnityContainerResolver
    {
        [Inject] private LifetimeScope _lifetimeScope;

        public T Resolve<T>()
        {
            return _lifetimeScope.Container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return _lifetimeScope.Container.Resolve(type);
        }
    }
}
