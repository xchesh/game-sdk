using UnityEngine;

namespace Core.Common.UnityContainer
{
    public interface IUnityContainer
    {
        IUnityContainerRegistrationBuilder Register<TConcrete>(UnityContainerScope scope = UnityContainerScope.Singleton);
        
        IUnityContainerRegistrationBuilder RegisterInstance<TConcrete>(TConcrete instance);
        IUnityContainerRegistrationBuilder RegisterInstance(object instance);
        
        IUnityContainerRegistrationBuilder RegisterComponentInNewPrefab<TConcrete>(TConcrete prefab, Transform parent = null, UnityContainerScope scope = UnityContainerScope.Singleton) where TConcrete : UnityEngine.Component;
        IUnityContainerRegistrationBuilder RegisterComponentOnNewGameObject<TConcrete>(string name = null, Transform parent = null, UnityContainerScope scope = UnityContainerScope.Singleton) where TConcrete : UnityEngine.Component;
    }
}