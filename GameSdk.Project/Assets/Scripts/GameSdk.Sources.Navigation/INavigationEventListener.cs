namespace GameSdk.Sources.Navigation
{
    public interface INavigationEventListener
    {
        void OnScreenEvent(IScreen screen, ScreenEvent screenEvent);
    }
}
