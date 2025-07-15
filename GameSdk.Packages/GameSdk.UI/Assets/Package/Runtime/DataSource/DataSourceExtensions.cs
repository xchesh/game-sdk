using UnityEngine.UIElements;

public static class DataSourceExtensions
{
    public static T ResolveSafe<T>(this VisualElement element)
    {
        var resolver = FindResolver(element);

        return resolver != null ? resolver.Resolve<T>() : default(T);
    }

    internal static IDataSourceResolver FindResolver(this VisualElement element)
    {
        if (element.dataSource is IDataSourceResolver localResolver && localResolver.IsInitialized)
        {
            return localResolver;
        }

        if (element.panel.visualTree.childCount > 0 && element.panel.visualTree[0].dataSource is IDataSourceResolver childResolver && childResolver.IsInitialized)
        {
            return childResolver;
        }

        if (element.panel.visualTree.dataSource is IDataSourceResolver rootResolver && rootResolver.IsInitialized)
        {
            return rootResolver;
        }

        return null;
    }
}