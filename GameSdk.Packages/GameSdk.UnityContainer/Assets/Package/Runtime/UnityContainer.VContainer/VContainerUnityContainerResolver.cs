using System;
using VContainer;
using VContainer.Unity;

namespace GameSdk.UnityContainer.VContainer
{
    [UnityEngine.Scripting.Preserve]
    public class VContainerUnityContainerResolver : IUnityContainerResolver
    {
        private readonly LifetimeScope _lifetimeScope;

        [UnityEngine.Scripting.RequiredMember]
        public VContainerUnityContainerResolver(LifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

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
