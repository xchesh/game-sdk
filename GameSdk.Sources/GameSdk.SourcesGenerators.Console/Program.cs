
using System.ComponentModel;
using System.Reflection;
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

        var notify = new TestNotify();

        notify.PropertyChanged += PropertyChanged;

        notify.TestString = "My test string change";
        notify.TestField = 5;
        notify.TestField = 1;
        notify.TestString = "test";

        notify.PropertyChanged -= PropertyChanged;

        notify.TestString = "Empty";
        notify.TestField = 0;
    }

    private static void PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        Console.WriteLine($"Property changed: {e.PropertyName}");
    }
}
