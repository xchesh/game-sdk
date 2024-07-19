using System.Linq;
using GameSdk.Sources.Generated;

namespace GameSdk.Sources
{
    public class TestClient
    {
        public TestClient()
        {
            UnityEngine.Debug.Log(JsonConverterReadCache.Converters.Count());
            UnityEngine.Debug.Log(JsonConvertableCache.Convertables.Count());
        }
    }

    [JsonConvertable("test_struct")]
    public partial struct TestStruct
    {

    }
}
namespace GameSdk.Sources.Generated
{
    [JsonConverterRead]
    public interface TestConverter
    {

    }
}
