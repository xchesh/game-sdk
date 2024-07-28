namespace GameSdk.Sources.Json
{
    internal class JsonGeneratorParams
    {
        internal static readonly string nameNamespace = SourceGeneratorParams.assemblyName + ".Generated";

        internal static readonly string nameConvertable = "JsonConvertable";
        internal static readonly string nameConverterRead = "JsonConverterRead";
    }
}
