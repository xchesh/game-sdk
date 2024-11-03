using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameSdk.UnityContainer.VContainer
{
    public class VContainerUnityContainer : IUnityContainer
    {
        public IContainerBuilder Builder { get; }

        public VContainerUnityContainer(IContainerBuilder builder)
        {
            Builder = builder;
        }

        public IUnityContainerBuilder Register<TConcrete>(
            UnityContainerScope scope = UnityContainerScope.Singleton)
        {
            return new VContainerUnityContainerBuilder(Builder.Register<TConcrete>(GetLifetime(scope)));
        }

        public IUnityContainerBuilder RegisterInstance<TConcrete>(TConcrete instance)
        {
            return new VContainerUnityContainerBuilder(Builder.RegisterInstance(instance));
        }

        public IUnityContainerBuilder RegisterInstance(object instance)
        {
            return new VContainerUnityContainerBuilder(Builder.RegisterInstance(instance));
        }

        public IUnityContainerBuilder RegisterComponentInNewPrefab<TConcrete>(TConcrete prefab,
            Transform parent = null, UnityContainerScope scope = UnityContainerScope.Singleton)
            where TConcrete : UnityEngine.Component
        {
            return new VContainerUnityContainerBuilder(Builder
                .RegisterComponentInNewPrefab<TConcrete>(prefab, GetLifetime(scope)).UnderTransform(parent));
        }

        public IUnityContainerBuilder RegisterComponentOnNewGameObject<TConcrete>(string name = null,
            Transform parent = null, UnityContainerScope scope = UnityContainerScope.Singleton)
            where TConcrete : UnityEngine.Component
        {
            return new VContainerUnityContainerBuilder(Builder
                .RegisterComponentOnNewGameObject<TConcrete>(GetLifetime(scope), name).UnderTransform(parent));
        }

        public static Lifetime GetLifetime(UnityContainerScope scope)
        {
            return scope switch
            {
                UnityContainerScope.Singleton => Lifetime.Singleton,
                UnityContainerScope.Transient => Lifetime.Transient,
                UnityContainerScope.Cached => Lifetime.Scoped,
                _ => throw new NotImplementedException()
            };
        }
    }
}
