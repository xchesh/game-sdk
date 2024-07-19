using GameSdk.Sources.Generated;
using Newtonsoft.Json;

namespace GameSdk.SourcesGenerators.Console
{
    [JsonConverterRead]

    public interface ITestInterface
    {

    }

    [JsonConvertable("test_struct")]
    public partial struct TestStruct : ITestInterface
    {
    }

    [JsonConvertableAttribute("test_class")]
    public partial class TestClass : ITestInterface
    {
    }

    [Serializable]
    public class TestSerializable
    {
        [JsonProperty("test_intrfc")] public ITestInterface intrfc;
    }

}
