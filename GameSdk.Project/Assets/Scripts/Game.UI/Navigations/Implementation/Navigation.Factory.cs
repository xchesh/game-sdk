using Game.UI.Navigations;

namespace Game.UI.Navigations
{
    public partial class Navigation
    {
        private T GetScreen<T>() where T : IScreen
        {
            var type = typeof(T);

            if (_screens.ContainsKey(type))
            {
                return (T)_screens[type];
            }

            return CreateScreen<T>();
        }

        private T CreateScreen<T>() where T : IScreen
        {
            var type = typeof(T);

            if (_screens.ContainsKey(type))
            {
                return (T)_screens[type];
            }

            var screen = _screenFactory.Create<T>(this, Component);

            _screens.Add(type, screen);

            return screen;
        }

        private IScreen CreateScreen(System.Type type)
        {
            if (_screens.ContainsKey(type))
            {
                return _screens[type];
            }

            var screen = _screenFactory.Create(type, this, Component);

            _screens.Add(type, screen);

            return screen;
        }

        private void DestroyScreen<T>(T screen) where T : IScreen
        {
            var type = typeof(T);

            if (_screens.ContainsKey(type))
            {
                _screenFactory.Destroy<T>(screen);

                _screens.Remove(type);
            }
        }
    }
}
