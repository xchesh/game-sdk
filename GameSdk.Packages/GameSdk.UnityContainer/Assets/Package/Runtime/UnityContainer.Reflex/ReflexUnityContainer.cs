
using Reflex.Core;
using UnityEngine;

namespace GameSdk.UnityContainer.Reflex
{
    public class ReflexUnityContainer : IUnityContainer
    {
        private readonly ContainerBuilder _builder;

        public ReflexUnityContainer(ContainerBuilder builder)
        {
            _builder = builder;
        }

        public IUnityContainerBuilder Register<TConcrete>(UnityContainerScope scope = UnityContainerScope.Singleton)
        {
            return new ReflexUnityContainerBuilder<TConcrete>(_builder, scope);
        }

        public IUnityContainerBuilder RegisterComponentInNewPrefab<TConcrete>(TConcrete prefab, Transform parent = null, UnityContainerScope scope = UnityContainerScope.Singleton) where TConcrete : Component
        {
            var instance = Object.Instantiate(prefab, parent);

            return new ReflexUnityContainerBuilderInstance<TConcrete>(instance, _builder, scope);
        }

        public IUnityContainerBuilder RegisterComponentOnNewGameObject<TConcrete>(string name = null, Transform parent = null, UnityContainerScope scope = UnityContainerScope.Singleton) where TConcrete : Component
        {
            if (string.IsNullOrEmpty(name))
            {
                name = typeof(TConcrete).Name;
            }

            var gameObject = new GameObject(name);

            if (parent != null)
            {
                gameObject.transform.SetParent(parent);
            }

            var instance = gameObject.AddComponent<TConcrete>();

            return new ReflexUnityContainerBuilderInstance<TConcrete>(instance, _builder, scope);
        }

        public IUnityContainerBuilder RegisterInstance<TConcrete>(TConcrete instance)
        {
            return new ReflexUnityContainerBuilderInstance<TConcrete>(instance, _builder, UnityContainerScope.Singleton);
        }

        public IUnityContainerBuilder RegisterInstance(object instance)
        {
            return new ReflexUnityContainerBuilderInstance<object>(instance, _builder, UnityContainerScope.Singleton);
        }
    }
}
