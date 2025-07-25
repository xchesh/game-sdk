namespace GameSdk.UI
{
    public partial class Navigation
    {
        private IDataSourceResolver _resolver;

        public T ResolveSafe<T>()
        {
            FindResolver();

            return _resolver != null ? _resolver.Resolve<T>() : default(T);
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