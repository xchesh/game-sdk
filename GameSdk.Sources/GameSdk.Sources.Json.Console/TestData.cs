using GameSdk.Sources.Json.Generated;
using Newtonsoft.Json;

namespace GameSdk.Sources.Json.Console
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

    [JsonNotify]
    public partial class TestNotify
    {
        [JsonProperty("MyTestField")]
        private int _testField;

        [JsonProperty]
        private string _testString;

        private int _testField2;
    }
}
