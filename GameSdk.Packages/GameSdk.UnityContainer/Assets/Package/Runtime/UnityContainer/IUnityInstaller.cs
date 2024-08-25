using UnityEngine;

namespace GameSdk.UnityContainer
{
    // ReSharper disable once InconsistentNaming
    public abstract class IUnityInstaller : ScriptableObject
    {
        public abstract void InstallBindings(IUnityContainer container);
    }
}
