using System.Collections.Generic;
using System.Linq;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

namespace GameSdk.UI.Navigation
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
            FindScreenAndConfig(type, out var screen, out var screenConfig);

            var visualTreeAsset = Addressables.LoadAssetAsync<VisualTreeAsset>(screenConfig.Asset).Result;

            screen.SetNavigation(navigation);
            screen.SetConfig(screenConfig);
            screen.SetVisualElement(visualTreeAsset.Instantiate());

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

            screen.SetVisualElement(null);
            screen.SetNavigation(null);
            screen.SetConfig(null);

            screen.Dispose();

        }

        private void FindScreenAndConfig(System.Type type, out IScreen screen, out IScreenConfig screenConfig)
        {
            screenConfig = _navigationConfig.GetScreenConfig(type);

            if (screenConfig == null)
            {
                throw new System.ArgumentException($"Screen config for {type.Name} not found");
            }

            var screenConfigType = screenConfig.Type;

            screen = _screens.FirstOrDefault(s => s.GetType() == screenConfigType);

            if (screen == null)
            {
                throw new System.ArgumentException($"Screen {screenConfigType.Name} not found");
            }
        }
    }
}
