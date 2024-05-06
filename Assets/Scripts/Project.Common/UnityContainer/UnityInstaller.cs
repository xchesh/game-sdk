using System.Collections.Generic;
using UnityEngine;

namespace Core.Common.UnityContainer
{
    [CreateAssetMenu(fileName = nameof(UnityInstaller), menuName = "Unity Container/" + nameof(UnityInstaller))]
    public partial class UnityInstaller : ScriptableObject
    {
        [SerializeField, HideInInspector, Space]
        private List<IUnityInstaller> _installers = new();

        public void InstallBindings(IUnityContainer container)
        {
            _installers.ForEach(installer => installer?.InstallBindings(container));
        }
    }
}
