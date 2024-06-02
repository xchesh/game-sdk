
using Reflex.Core;
using UnityEngine;

namespace Project.Common.UnityContainer.Reflex
{
    public class ReflexUnityContainer : IUnityContainer
    {
        private readonly ContainerBuilder _builder;

        public ReflexUnityContainer(ContainerBuilder builder)
        {
            _builder = builder;
        }

        public IUnityContainerRegistrationBuilder Register<TConcrete>(UnityContainerScope scope = UnityContainerScope.Singleton)
        {
            return new ReflexRegistrationBuilder<TConcrete>(_builder, scope);
        }

        public IUnityContainerRegistrationBuilder RegisterComponentInNewPrefab<TConcrete>(TConcrete prefab, Transform parent = null, UnityContainerScope scope = UnityContainerScope.Singleton) where TConcrete : Component
        {
            var instance = Object.Instantiate(prefab, parent);

            return new ReflexRegistrationBuilderInstance<TConcrete>(instance, _builder, scope);
        }

        public IUnityContainerRegistrationBuilder RegisterComponentOnNewGameObject<TConcrete>(string name = null, Transform parent = null, UnityContainerScope scope = UnityContainerScope.Singleton) where TConcrete : Component
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

            return new ReflexRegistrationBuilderInstance<TConcrete>(instance, _builder, scope);
        }

        public IUnityContainerRegistrationBuilder RegisterInstance<TConcrete>(TConcrete instance)
        {
            return new ReflexRegistrationBuilderInstance<TConcrete>(instance, _builder, UnityContainerScope.Singleton);
        }

        public IUnityContainerRegistrationBuilder RegisterInstance(object instance)
        {
            return new ReflexRegistrationBuilderInstance<object>(instance, _builder, UnityContainerScope.Singleton);
        }
    }
}
