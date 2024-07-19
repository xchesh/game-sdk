
using GameSdk.Sources.Generated;
using GameSdk.SourcesGenerators.Console;
using Newtonsoft.Json;

public class Program
{
    public static void Main()
    {
        Console.WriteLine($"Convertables count: {JsonConvertableCache.Convertables.Count()}");
        Console.WriteLine($"ConvertersRead count: {JsonConverterReadCache.Converters.Count()}");

        var convertables = JsonConvertableCache.Convertables;
        var testStruct = new TestStruct();
        var testClass = new TestClass();

        Console.WriteLine($"TestStruct: {convertables[testStruct.Key]}");
        Console.WriteLine($"TestClass: {convertables[testClass.Key]}");

        var jsonSerializerSettings = new JsonSerializerSettings()
        {
            Converters = JsonConverterReadCache.Converters,
        };

        var json = JsonConvert.SerializeObject(new TestSerializable() { intrfc = new TestStruct() }, jsonSerializerSettings);

        Console.WriteLine(json);

        var result = JsonConvert.DeserializeObject<TestSerializable>(json, jsonSerializerSettings);

        Console.WriteLine($"Result: " + result.intrfc.ToString());
    }
}
