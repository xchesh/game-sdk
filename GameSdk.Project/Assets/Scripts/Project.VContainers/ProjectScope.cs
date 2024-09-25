using GameSdk.UnityContainer.VContainer;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace Project.VContainers
{
    public class ProjectScope : VContainerScope
    {
        [SerializeField] private UIDocument _document;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_document).As<UIDocument>();

            builder.Register<DataSource>(Lifetime.Singleton);
            builder.Register<DataSourceResolver>(Lifetime.Singleton).As<IDataSourceResolver>();

            builder.RegisterBuildCallback(container =>
            {
                _document.rootVisualElement.dataSource = container.Resolve<IDataSourceResolver>();
            });
        }
    }
}
