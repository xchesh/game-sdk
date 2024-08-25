namespace GameSdk.Services.RemoteConfig
{
    public interface IRemoteConfig
    {
        string[] GetKeys();
        bool GetBool(string key, bool defaultValue = false);
        float GetFloat(string key, float defaultValue = 0.0F);
        int GetInt(string key, int defaultValue = 0);
        string GetString(string key, string defaultValue = "");
        long GetLong(string key, long defaultValue = 0L);
        bool HasKey(string key);
        string GetJson(string key, string defaultValue = "{}");
        T GetValue<T>(string key, T defaultValue = default);
    }
}
