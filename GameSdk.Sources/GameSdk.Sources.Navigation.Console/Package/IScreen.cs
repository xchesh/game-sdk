using UnityEngine.UIElements;

namespace GameSdk.Sources.Navigation
{
    public interface IScreen : IDisposable
    {
        INavigation Navigation { get; }
        IScreenConfig Config { get; }
        VisualElement VisualElement { get; }

        bool IsActive { get; }
        bool IsVisible { get; }

        void OnCreate();

        void OnShow(params object[] parameters);
        void OnFocus();
        void OnBlur();
        void OnHide();

        internal void SetNavigation(INavigation navigation);
        internal void SetConfig(IScreenConfig config);
        internal void SetVisualElement(VisualElement visualElement);
        internal void SetIsActive(bool isActive);
        internal void SetIsVisible(bool isVisible);
    }
}
