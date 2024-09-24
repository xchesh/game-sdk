using System.Collections;
using System;
using UnityEngine.UIElements;

namespace GameSdk.UI.Navigation
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

        void SetNavigation(INavigation navigation);
        void SetConfig(IScreenConfig config);
        void SetVisualElement(VisualElement visualElement);
        void SetIsActive(bool isActive);
        void SetIsVisible(bool isVisible);
    }
}
