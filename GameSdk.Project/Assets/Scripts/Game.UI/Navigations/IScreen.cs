using System;
using UnityEngine.UIElements;

namespace Game.UI.Navigations
{
    public interface IScreen : IDisposable
    {
        INavigation Navigation { get; set; }
        IScreenConfig Config { get; set; }
        VisualElement VisualElement { get; set; }

        bool IsActive { get; set; }
        bool IsVisible { get; set; }

        void OnCreate();

        void OnShow(params object[] parameters);
        void OnFocus();
        void OnBlur();
        void OnHide();
    }
}
