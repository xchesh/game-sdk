using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GameSdk.Sources.Json
{
    public partial class JsonGenerator
    {
        // Generate cache file of convertables for prevent runtime reflecion
        private (string fileName, string fileContent) GetConvertablesCache(Dictionary<string, string> convertables)
        {
            var fileName = $"{JsonGeneratorParams.nameConvertable}Cache.g.cs";
            var fileContent = new StringBuilder($@"
using System;
using System.Collections.Generic;

namespace {JsonGeneratorParams.nameNamespace}
{{
    public sealed class {JsonGeneratorParams.nameConvertable}Cache
    {{
        public static IDictionary<string, Type> Convertables = new Dictionary<string, Type>()
        {{
");
            foreach (var item in convertables)
            {
                fileContent.AppendLine($@"            {{ ""{item.Key}"", typeof({item.Value})}}, ");
            }

            fileContent.AppendLine($@"
        }};
    }}
}}
");
            return (fileName, fileContent.ToString());
        }

        // Generate single partial convertable type with key property
        private (string fileName, string fileContent, string key, string fullName) GetConvertable(TypeDeclarationSyntax typeDeclarationSyntax)
        {
            var (typeName, typeNamespace, declarationType) = typeDeclarationSyntax.GetTypeDeclaration();
            var attribute = typeDeclarationSyntax.AttributeLists.SelectMany(a => a.Attributes).First(a => SourceGeneratorParams.ContainsAttribute(a.Name.ToString(), JsonGeneratorParams.nameConvertable));
            var attributeArg1 = attribute.ArgumentList.Arguments.First().Expression.ToString().Trim('"');

            var modifiers = string.Join(" ", typeDeclarationSyntax.Modifiers); // Save original modifiers
            var fullName = $"global::{typeNamespace}.{typeName}";
            var fileName = $"{typeName}.g.cs";
            var fileContent = $@"
using System;
using Newtonsoft.Json;

namespace {typeNamespace}
{{
    [JsonObject(MemberSerialization.OptIn)]
    {modifiers} {declarationType} {typeName}
    {{
        [JsonProperty(""key"")]
        public string Key => ""{attributeArg1}"";
    }}
}}
";
            return (fileName, fileContent, attributeArg1, fullName);
        }

        private (List<(string fileName, string fileContent)> files, Dictionary<string, string> map) GetConvertablesAll(List<TypeDeclarationSyntax> candidates)
        {
            var convertablesDict = new Dictionary<string, string>();
            var convertablesList = new List<(string, string)>();

            foreach (var candidate in candidates)
            {
                var (fileName, fileContent, key, fullName) = GetConvertable(candidate);

                convertablesDict.Add(key, fullName);
                convertablesList.Add((fileName, fileContent));
            }

            return (convertablesList, convertablesDict);
        }
    }
}
