namespace GameSdk.Services.RemoteConfig
{
    public interface IRemoteConfigAttribution
    {
        (IUserAttributes user, IAppAttributes app, IFilterAttributes filter) GetAttributes();

        IUserAttributes GetUserAttributes();
        IAppAttributes GetAppAttributes();
        IFilterAttributes GetFilterAttributes();
    }
}