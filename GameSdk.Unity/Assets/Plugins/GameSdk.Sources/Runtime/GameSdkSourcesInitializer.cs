using UnityEngine;
using GameSdk.Sources.Generated;

namespace GameSdk.Sources
{
    // Requires script with same namespace as in sources generator
    public class GameSdkSourcesInitializer
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
