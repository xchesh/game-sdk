using System.Collections.Generic;
using UnityEngine;

namespace Project.Common.UnityContainer
{
    [CreateAssetMenu(fileName = nameof(UnityInstaller), menuName = "Unity Installer")]
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
