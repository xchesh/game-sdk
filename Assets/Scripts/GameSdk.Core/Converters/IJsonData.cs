using Newtonsoft.Json;

namespace GameSdk.Core.Converters
{
    public interface IJsonData
    {
        const string KEY = "key";

        [JsonProperty(KEY)]
        string Key { get; }
    }
}