namespace GameSdk.UI
{
    public partial class Navigation
    {
        private IDataSourceResolver _resolver;

        protected T ResolveSafe<T>()
        {
            FindResolver();

            return _resolver.Resolve<T>();
        }

        private void FindResolver()
        {
            if (dataSource is IDataSourceResolver localResolver && localResolver.IsInitialized)
            {
                _resolver = localResolver;
                return;
            }

            if (panel.visualTree.childCount > 0 && panel.visualTree[0].dataSource is IDataSourceResolver childResolver && childResolver.IsInitialized)
            {
                _resolver = childResolver;
                return;
            }

            if (panel.visualTree.dataSource is IDataSourceResolver rootResolver && rootResolver.IsInitialized)
            {
                _resolver = rootResolver;
                return;
            }
        }
    }
}