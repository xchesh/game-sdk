using UnityEngine;
using GameSdk.Sources.Json.Generated;

namespace GameSdk.Sources.Json
{
    // Requires script with same namespace as in sources generator
    public class GameSdkSourcesJsonInitializer
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        static void Initialize()
        {
            var convertablesCount = JsonConvertableCache.Convertables.Count;

            if (convertablesCount > 0)
            {
                Debug.Log($"Registered {convertablesCount} convertables. ; Read converters: {JsonConverterReadCache.Converters.Count}");
            }

            var readConvertersCount = JsonConverterReadCache.Converters.Count;

            if (readConvertersCount > 0)
            {
                Debug.Log($"Registered {readConvertersCount} read converters");
            }
        }
    }

}
