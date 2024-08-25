#if UNITY_SERVICES_CORE && UNITY_SERVICES_REMOTE_CONFIG

using GameSdk.Services.RemoteConfig;
using Unity.Services.RemoteConfig;
using UnityEngine;

namespace GameSdk.Services.Unity
{
    public class UnityRemoteConfig : IRemoteConfig
    {
        private readonly RuntimeConfig _config;

        public UnityRemoteConfig(RuntimeConfig config)
        {
            _config = config;
        }

        public string[] GetKeys()
        {
            return _config.GetKeys();
        }

        public bool GetBool(string key, bool defaultValue = false)
        {
            return _config.GetBool(key, defaultValue);
        }

        public float GetFloat(string key, float defaultValue = 0)
        {
            return _config.GetFloat(key, defaultValue);
        }

        public int GetInt(string key, int defaultValue = 0)
        {
            return _config.GetInt(key, defaultValue);
        }

        public string GetString(string key, string defaultValue = "")
        {
            return _config.GetString(key, defaultValue);
        }

        public long GetLong(string key, long defaultValue = 0)
        {
            return _config.GetLong(key, defaultValue);
        }

        public bool HasKey(string key)
        {
            return _config.HasKey(key);
        }

        public string GetJson(string key, string defaultValue = "{}")
        {
            return _config.GetJson(key, defaultValue);
        }

        public T GetValue<T>(string key, T value)
        {
            var jsonString = _config.GetJson(key, "{}");

            JsonUtility.FromJsonOverwrite(jsonString, value);

            return value;
        }
    }
}

#endif
