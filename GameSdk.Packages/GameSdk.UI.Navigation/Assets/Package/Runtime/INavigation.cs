using UnityEngine.UIElements;

namespace GameSdk.UI.Navigation
{
    public interface INavigation
    {
        const string TAG = "Navigation";

        IScreen Current { get; }

        void Initialize(INavigationConfig navigationConfig, VisualElement visualElement, INavigation parent = null);

        void RegisterEventListener(INavigationEventListener eventListeners);
        void UnregisterEventListener(INavigationEventListener eventListeners);

        T Replace<T>(params object[] parameters) where T : IScreen;
        T Replace<T>(T screen, params object[] parameters) where T : IScreen;

        T Push<T>(params object[] parameters) where T : IScreen;
        T Push<T>(T screen, params object[] parameters) where T : IScreen;

        T PopTo<T>() where T : IScreen;
        void Pop();
    }
}
