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
    }
}
