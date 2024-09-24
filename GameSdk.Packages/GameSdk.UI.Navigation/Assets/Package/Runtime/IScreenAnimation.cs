namespace GameSdk.UI.Navigation
{
    public interface IScreenAnimation
    {
        void AnimateShow(System.Action<IScreen> callback);
        void AnimateHide(System.Action<IScreen> callback);
    }
}
