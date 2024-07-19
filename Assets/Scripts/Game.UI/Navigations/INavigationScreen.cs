using GameSdk.Core.Common;

namespace Game.UI.Navigations
{
    public interface INavigationScreen : INavigationScreenActions
    {
        void OnShow();
        void OnHide();

        void OnBlur();
        void OnFocus();
    }
}
