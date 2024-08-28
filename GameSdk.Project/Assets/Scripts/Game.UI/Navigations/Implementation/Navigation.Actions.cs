using UnityEditor.Localization.Plugins.XLIFF.V20;

namespace Game.UI.Navigations
{
    public partial class Navigation
    {
        private T ShowScreen<T>(T screen, params object[] parameters) where T : IScreen
        {
            Current = screen;

            if (screen.IsActive is true)
            {
                return screen;
            }

            if (screen.IsVisible is false)
            {
                ShowScreen_Action(screen, parameters);
            }

            if (screen is IScreenAnimation screenAnimation)
            {
                screenAnimation.AnimateShow(FocusScreen_Action);

                return screen;
            }

            FocusScreen_Action(screen);

            return screen;
        }

        private void HideScreen<T>(T screen) where T : IScreen
        {
            if (screen.IsActive)
            {
                BlurScreen_Action(screen);
            }

            if (screen is IScreenAnimation screenAnimation)
            {
                screenAnimation.AnimateHide(HideScreen_Action);

                return;
            }

            HideScreen_Action(screen);
        }

        private void FocusScreen_Action<T>(T screen) where T : IScreen
        {
            screen.IsActive = true;
            screen.OnFocus();

            NotifyEventListeners(screen, ScreenEvent.Focus);
        }

        private void BlurScreen_Action<T>(T screen) where T : IScreen
        {
            screen.IsActive = false;
            screen.OnBlur();

            NotifyEventListeners(screen, ScreenEvent.Blur);
        }

        private void ShowScreen_Action<T>(T screen, params object[] parameters) where T : IScreen
        {
            screen.IsVisible = true;
            screen.VisualElement.BringToFront();

            screen.OnShow(parameters);

            NotifyEventListeners(screen, ScreenEvent.Show);
        }

        private void HideScreen_Action<T>(T screen) where T : IScreen
        {
            screen.IsVisible = false;
            screen.OnHide();

            NotifyEventListeners(screen, ScreenEvent.Hide);
        }
    }
}
