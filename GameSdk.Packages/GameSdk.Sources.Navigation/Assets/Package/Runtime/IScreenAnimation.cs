namespace GameSdk.Sources.Navigation
{
    public interface IScreenAnimation
    {
        void AnimateShow(System.Action<IScreen> callback);
        void AnimateHide(System.Action<IScreen> callback);
    }
}
