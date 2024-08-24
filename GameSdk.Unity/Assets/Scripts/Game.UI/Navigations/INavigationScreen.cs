using GameSdk.Sources.Core.Common;

namespace Game.UI.Navigations
{
    public interface INavigationScreen : INavigationScreenActions
    {
        void OnShow();
        void OnHide();

        void OnBlur();
        void OnFocus();

        void INavigationScreenActions.Show()
        {
            OnShow();
            OnFocus();
        }

        void INavigationScreenActions.Hide()
        {
            OnBlur();
            OnHide();
        }
    }
}
