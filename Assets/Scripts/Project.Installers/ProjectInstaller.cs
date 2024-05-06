using Core.Common.UnityContainer;
using UnityEngine;

namespace Project.Installers
{
    public class ProjectInstaller : IUnityInstaller
    {
        [SerializeField] private GameObject _prefab;

        public override void InstallBindings(IUnityContainer container)
        {
        }
    }
}
