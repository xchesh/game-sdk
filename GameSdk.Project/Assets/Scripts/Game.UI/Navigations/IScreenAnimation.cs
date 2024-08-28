namespace Game.UI.Navigations
{
    public interface IScreenAnimation
    {
        void AnimateShow(System.Action<IScreen> callback);
        void AnimateHide(System.Action<IScreen> callback);
    }
}
