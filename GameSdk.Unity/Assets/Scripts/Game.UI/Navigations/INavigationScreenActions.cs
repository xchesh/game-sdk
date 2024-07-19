using GameSdk.Core.Common;

namespace Game.UI.Navigations
{
    public interface INavigationScreenActions
    {
        internal void Show(System.Action callback = null);
        internal void Hide(System.Action callback = null);

        internal void Blur(System.Action callback = null);
        internal void Focus(System.Action callback = null);
    }
}
