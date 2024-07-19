namespace GameSdk.SourcesGenerators
{
    internal class JsonGeneratorParams
    {
        internal static readonly string namespaceGenerated = "GameSdk.Sources.Generated";

        internal static readonly string nameConvertable = "JsonConvertable";
        internal static readonly string nameConverterRead = "JsonConverterRead";

        internal static bool ContainsAttribute(string name, string attribute)
        {
            return name.Replace("Attribute", "") == attribute;
        }
    }
}
