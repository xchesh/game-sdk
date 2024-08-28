using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

namespace Game.UI.Navigations
{
    public class ScreenFactory : IScreenFactory
    {
        private readonly INavigationConfig _navigationConfig;
        private readonly IEnumerable<IScreen> _screens;

        public ScreenFactory(INavigationConfig navigationConfig, IEnumerable<IScreen> screens)
        {
            _navigationConfig = navigationConfig;
            _screens = screens;
        }

        public IScreen Create(System.Type type, INavigation navigation, VisualElement parent)
        {
            var screenConfig = _navigationConfig.GetScreenConfig(type);

            if (screenConfig == null)
            {
                throw new System.ArgumentException($"Screen config for {type.Name} not found");
            }

            var screen = _screens.FirstOrDefault(s => s.GetType() == screenConfig.Type);

            if (screen == null)
            {
                throw new System.ArgumentException($"Screen {screenConfig.Type.Name} not found");
            }

            screen.Navigation = navigation;
            screen.Config = screenConfig;
            screen.VisualElement = screenConfig.Asset.Instantiate();

            parent.Add(screen.VisualElement);

            return screen;
        }

        public T Create<T>(INavigation navigation, VisualElement parent) where T : IScreen
        {
            return (T)Create(typeof(T), navigation, parent);
        }

        public void Destroy<T>(T screen) where T : IScreen
        {
            screen.VisualElement.RemoveFromHierarchy();
            screen.VisualElement = null;
            screen.Navigation = null;
            screen.Config = null;

            screen.Dispose();

        }
    }
}
