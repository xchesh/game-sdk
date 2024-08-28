namespace Game.UI.Navigations
{
    public interface INavigationEventListener
    {
        void OnScreenEvent(IScreen screen, ScreenEvent screenEvent);
    }
}
