using System.Linq;
using Microsoft.CodeAnalysis;

namespace GameSdk.SourcesGenerators
{
    internal class JsonGeneratorParams
    {
        internal static readonly string assemblyName = "GameSdk.Sources";
        internal static readonly string assemblyNameConsole = "GameSdk.SourcesGenerators.Console";

        internal static readonly string namespaceGenerated = "GameSdk.Sources.Generated";

        internal static readonly string nameConvertable = "JsonConvertable";
        internal static readonly string nameConverterRead = "JsonConverterRead";

        internal static bool ContainsAttribute(string name, string attribute)
        {
            return name.Replace("Attribute", "") == attribute;
        }

        internal static bool ContainsAssembly(string assemblyName)
        {
            return assemblyName == JsonGeneratorParams.assemblyNameConsole || assemblyName == JsonGeneratorParams.assemblyName;
        }

        internal static bool ContainsAssembly(Compilation compilation)
        {
            return compilation.AssemblyName == JsonGeneratorParams.assemblyNameConsole
                || compilation.AssemblyName == JsonGeneratorParams.assemblyName
                || compilation.ReferencedAssemblyNames.Any(a => ContainsAssembly(a.Name));
        }
    }
}
