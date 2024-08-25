namespace GameSdk.Sources.Services.RemoteConfig
{
    public interface IRemoteConfigListener
    {
        void OnConfigFetched<T>(T config) where T : IRemoteConfig;
    }
}
