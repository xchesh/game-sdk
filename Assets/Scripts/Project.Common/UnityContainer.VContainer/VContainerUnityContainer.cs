using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Common.UnityContainer
{
    public class VContainerUnityContainer : IUnityContainer
    {
        public IContainerBuilder Builder { get; }

        public VContainerUnityContainer(IContainerBuilder builder)
        {
            Builder = builder;
        }

        public IUnityContainerRegistrationBuilder Register<TConcrete>(
            UnityContainerScope scope = UnityContainerScope.Singleton)
        {
            return new VContainerRegistrationBuilder(Builder.Register<TConcrete>(GetLifetime(scope)));
        }

        public IUnityContainerRegistrationBuilder RegisterInstance<TConcrete>(TConcrete instance)
        {
            return new VContainerRegistrationBuilder(Builder.RegisterInstance(instance));
        }

        public IUnityContainerRegistrationBuilder RegisterInstance(object instance)
        {
            return new VContainerRegistrationBuilder(Builder.RegisterInstance(instance));
        }

        public IUnityContainerRegistrationBuilder RegisterComponentInNewPrefab<TConcrete>(TConcrete prefab,
            Transform parent = null, UnityContainerScope scope = UnityContainerScope.Singleton)
            where TConcrete : UnityEngine.Component
        {
            return new VContainerRegistrationBuilder(Builder
                .RegisterComponentInNewPrefab<TConcrete>(prefab, GetLifetime(scope)).UnderTransform(parent));
        }

        public IUnityContainerRegistrationBuilder RegisterComponentOnNewGameObject<TConcrete>(string name = null,
            Transform parent = null, UnityContainerScope scope = UnityContainerScope.Singleton)
            where TConcrete : UnityEngine.Component
        {
            return new VContainerRegistrationBuilder(Builder
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
