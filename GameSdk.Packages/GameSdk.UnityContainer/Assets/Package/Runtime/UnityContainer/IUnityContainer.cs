using UnityEngine;

namespace GameSdk.UnityContainer
{
    public interface IUnityContainer
    {
        IUnityContainerBuilder Register<TConcrete>(UnityContainerScope scope = UnityContainerScope.Singleton);

        IUnityContainerBuilder RegisterInstance<TConcrete>(TConcrete instance);
        IUnityContainerBuilder RegisterInstance(object instance);

        IUnityContainerBuilder RegisterComponentInNewPrefab<TConcrete>(TConcrete prefab, Transform parent = null, UnityContainerScope scope = UnityContainerScope.Singleton) where TConcrete : UnityEngine.Component;
        IUnityContainerBuilder RegisterComponentOnNewGameObject<TConcrete>(string name = null, Transform parent = null, UnityContainerScope scope = UnityContainerScope.Singleton) where TConcrete : UnityEngine.Component;
    }
}
