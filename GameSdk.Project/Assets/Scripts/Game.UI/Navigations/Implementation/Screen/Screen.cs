using UnityEngine.UIElements;

namespace Game.UI.Navigations
{
    public class Screen : IScreen
    {
        public virtual INavigation Navigation { get; set; }
        public virtual IScreenConfig Config { get; set; }
        public virtual VisualElement VisualElement { get; set; }

        public virtual bool IsActive { get; set; }
        public virtual bool IsVisible { get; set; }

        public virtual void OnCreate() { }

        public virtual void OnShow(params object[] parameters) { }
        public virtual void OnFocus() { }
        public virtual void OnBlur() { }
        public virtual void OnHide() { }

        public virtual void Dispose() { }
    }
}
