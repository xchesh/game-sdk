using UnityEngine;

namespace Core.Common.UnityContainer
{
    public abstract class IUnityInstaller : ScriptableObject
    {
        public abstract void InstallBindings(IUnityContainer container);
    }
}
