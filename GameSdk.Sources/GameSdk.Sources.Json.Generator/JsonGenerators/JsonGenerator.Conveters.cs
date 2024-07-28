using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GameSdk.Sources.Json
{
    public partial class JsonGenerator
    {
        // Generate JsonConverterRead
        private (string fileName, string fileContent) GetConverterRead()
        {
            var fileName = $"{JsonGeneratorParams.nameConverterRead}.g.cs";
            var fileContent = $@"
using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace {JsonGeneratorParams.nameNamespace}
{{
    public class {JsonGeneratorParams.nameConverterRead}<T> : JsonConverter
    {{
        public override bool CanWrite => false;
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotSupportedException(""JsonConverterRead should only be used while deserializing."");

        public override bool CanConvert(Type objectType) => typeof(T) == objectType;
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {{
            var flag = existingValue == null;

            if (!flag && existingValue is not T)
            {{
                throw new JsonSerializationException(string.Format(CultureInfo.InvariantCulture, ""Converter cannot read JSON with the specified existing value. {{0}} is required."", typeof(T)));
            }}

            try
            {{
                var data = JObject.Load(reader);

                if (data.TryGetValue(""key"", out var jKey))
                {{
                    var key = jKey.Value<string>();
                    data.Remove(""key"");

                    var types = {JsonGeneratorParams.nameConvertable}Cache.Convertables;

                    if (types.TryGetValue(key, out var type))
                    {{
                        return (T)data.ToObject(type, serializer);
                    }}

                    throw new Exception($""Element with type '{{key}}' not found"");
                }}

                throw new Exception($""Element should have 'key' field"");
            }}
            catch (Exception e)
            {{
#if UNITY_2021_1_OR_NEWER
                UnityEngine.Debug.LogException(e);
#endif
                return default;
            }}
        }}
    }}
}}
";
            return (fileName, fileContent);
        }

        // Generate JsonConverterReadAttribute used JsonGeneratorParams
        private (string fileName, string fileContent) GetConverterReadAttribute()
        {
            var fileName = $"{JsonGeneratorParams.nameConverterRead}Attribute.g.cs";
            var fileContent = $@"
using System;

namespace {JsonGeneratorParams.nameNamespace}
{{
    [AttributeUsage(AttributeTargets.Interface, Inherited = false, AllowMultiple = false)]
    public sealed class {JsonGeneratorParams.nameConverterRead}Attribute : Attribute
    {{
    }}
}}
";
            return (fileName, fileContent);
        }

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
    internal sealed class {JsonGeneratorParams.nameConverterRead}Cache
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
