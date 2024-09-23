using UnityEngine.UIElements;

namespace GameSdk.Sources.Navigation
{
    public abstract class ScreenBase : IScreen
    {
        public virtual INavigation Navigation { get; private set; }
        public virtual IScreenConfig Config { get; private set; }
        public virtual VisualElement VisualElement { get; private set; }

        public virtual bool IsActive { get; private set; }
        public virtual bool IsVisible { get; private set; }

        public abstract void OnBlur();
        public abstract void OnCreate();
        public abstract void OnFocus();
        public abstract void OnHide();
        public abstract void OnShow(params object[] parameters);

        public virtual void SetConfig(IScreenConfig config) => Config = config;
        public virtual void SetIsActive(bool isActive) => IsActive = isActive;
        public virtual void SetIsVisible(bool isVisible) => IsVisible = isVisible;
        public virtual void SetNavigation(INavigation navigation) => Navigation = navigation;
        public virtual void SetVisualElement(VisualElement visualElement) => VisualElement = visualElement;

        public virtual void Dispose()
        {
            Navigation = null;
            Config = null;
            VisualElement = null;
        }
    }
}
