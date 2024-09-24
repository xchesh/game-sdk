namespace GameSdk.UI.Navigation
{
    public interface INavigationEventListener
    {
        void OnScreenEvent(IScreen screen, ScreenEvent screenEvent);
    }
}
