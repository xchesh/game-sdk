using UnityEngine.UIElements;

namespace GameSdk.UI
{
    public static class NavigationsExtensions
    {
        public static Screen Push(this Navigation navigation, System.Type type, object data = null)
        {
            var screen = navigation.GetScreen(type);

            return navigation.Push(screen, data);
        }

        public static T Push<T>(this Navigation navigation, object data = null) where T : Screen
        {
            return navigation.Push(typeof(T), data) as T;
        }

        public static Screen Replace(this Navigation navigation, System.Type screenType, object data = null)
        {
            var screen = navigation.GetScreen(screenType);

            return navigation.Replace(screen, data);
        }

        public static T Replace<T>(this Navigation navigation, object data = null) where T : Screen
        {
            return navigation.Replace(typeof(T), data) as T;
        }

        public static TNew ReplaceTo<TOld, TNew>(this Navigation navigation, object data = null) where TNew : Screen where TOld : Screen
        {
            return navigation.ReplaceTo(navigation.GetScreen(typeof(TOld)), navigation.GetScreen(typeof(TNew)), data) as TNew;
        }

        public static T ResolveSafe<T>(this Screen screen)
        {
            var resolver = FindResolver(screen);

            return resolver != null ? resolver.Resolve<T>() : default(T);
        }

        internal static IDataSourceResolver FindResolver(this Screen screen)
        {
            if (screen.dataSource is IDataSourceResolver localResolver && localResolver.IsInitialized)
            {
                return localResolver;
            }

            if (screen.panel.visualTree.childCount > 0 && screen.panel.visualTree[0].dataSource is IDataSourceResolver childResolver && childResolver.IsInitialized)
            {
                return childResolver;
            }

            if (screen.panel.visualTree.dataSource is IDataSourceResolver rootResolver && rootResolver.IsInitialized)
            {
                return rootResolver;
            }

            return null;
        }
    }
}
