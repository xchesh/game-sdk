using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GameSdk.Sources.Json
{
    public partial class JsonGenerator
    {
        private (string fileName, string fileContent, string name) GetConverterRead(TypeDeclarationSyntax typeDeclarationSyntax)
        {
            var (typeName, typeNamespace, declarationType) = typeDeclarationSyntax.GetTypeDeclaration();

            var name = JsonGeneratorParams.nameConverterRead + typeName;
            var fullName = $"global::{typeNamespace}.{typeName}";
            var fileName = $"{JsonGeneratorParams.nameConverterRead}{typeName}.g.cs";
            var fileContent = $@"
namespace {JsonGeneratorParams.nameNamespace}
{{
    internal class {name} : {JsonGeneratorParams.nameConverterRead}<{fullName}> {{ }}
}}
";
            return (fileName, fileContent, name);
        }

        // Generate cache file of convertables for prevent runtime reflecion
        private (string fileName, string fileContent) GetConvertersReadCache(List<(string, string, string name)> converters)
        {
            var fileName = $"{JsonGeneratorParams.nameConverterRead}Cache.g.cs";
            var fileContent = new StringBuilder($@"
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace {JsonGeneratorParams.nameNamespace}
{{
    public sealed class {JsonGeneratorParams.nameConverterRead}Cache
    {{
        public static IList<JsonConverter> Converters = new List<JsonConverter>()
        {{
");
            foreach (var converter in converters)
            {
                fileContent.AppendLine($@"            new {converter.name}(), ");
            }

            fileContent.AppendLine($@"
        }};
    }}
}}
");
            return (fileName, fileContent.ToString());
        }

        private List<(string fileName, string fileContent, string name)> GetConvertersReadAll(List<TypeDeclarationSyntax> candidates)
        {
            return candidates.Select(GetConverterRead).ToList();
        }
    }
}
