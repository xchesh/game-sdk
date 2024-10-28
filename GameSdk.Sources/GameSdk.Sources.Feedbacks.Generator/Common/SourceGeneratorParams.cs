using System.Linq;
using Microsoft.CodeAnalysis;

namespace GameSdk.Sources.Feedbacks.Generator
{
    internal class SourceGeneratorParams
    {
        internal static readonly string assemblyName = "GameSdk.Sources.Feedbacks";
        internal static readonly string assemblyNameConsole = "GameSdk.Sources.Feedbacks.Console";

        internal static bool ContainsAttribute(string name, string attribute)
        {
            return name.Replace("Attribute", "") == attribute.Replace("Attribute", "");
        }

        internal static bool ContainsAssembly(string assembly)
        {
            return assembly == assemblyNameConsole || assembly == assemblyName;
        }

        internal static bool ContainsAssembly(Compilation compilation)
        {
            return compilation.AssemblyName == assemblyNameConsole
                || compilation.AssemblyName == assemblyName
                || compilation.ReferencedAssemblyNames.Any(a => ContainsAssembly(a.Name));
        }
    }
}
