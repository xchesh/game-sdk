using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GameSdk.SourcesGenerators
{

    [Generator]
    public partial class JsonGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxReceiver is JsonGeneratorSyntaxReceiver jsonConvertableReceiver)
            {
                // Generate converters READ and Cache
                var convertersRead = GetConvertersReadAll(jsonConvertableReceiver.Converters);
                var convertersReadCache = GetConvertersReadCache(convertersRead);

                // Generate partial types with Key property and Type Cache
                var convertables = GetConvertablesAll(jsonConvertableReceiver.Convertables);
                var convertablesCache = GetConvertablesCache(convertables.map);

                foreach (var converter in convertersRead)
                {
                    context.AddSource(converter.fileName, converter.fileContent);
                }

                foreach (var convertable in convertables.files)
                {
                    context.AddSource(convertable.fileName, convertable.fileContent);
                }

                context.AddSource(convertersReadCache.fileName, convertersReadCache.fileContent);
                context.AddSource(convertablesCache.fileName, convertablesCache.fileContent);
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new JsonGeneratorSyntaxReceiver());
            context.RegisterForPostInitialization((i) =>
            {
                var converterRead = GetConverterRead();
                var attributeConvertable = GetConvertableAttribute();
                var attributeConverterRead = GetConverterReadAttribute();

                i.AddSource(converterRead.fileName, converterRead.fileContent);
                i.AddSource(attributeConvertable.fileName, attributeConvertable.fileContent);
                i.AddSource(attributeConverterRead.fileName, attributeConverterRead.fileContent);
            });
        }

        private static string GetDeclarationType(TypeDeclarationSyntax typeDeclarationSyntax)
        {
            return typeDeclarationSyntax switch
            {
                ClassDeclarationSyntax => "class",
                InterfaceDeclarationSyntax => "interface",
                StructDeclarationSyntax => "struct",
                _ => "record"
            };
        }

        private static (string typeName, string typeNamespace, string declarationType) GetTypeDeclaration(TypeDeclarationSyntax typeDeclarationSyntax)
        {

            var declarationType = GetDeclarationType(typeDeclarationSyntax);
            var typeName = typeDeclarationSyntax.Identifier.Text;
            var typeNamespace = typeDeclarationSyntax.Ancestors().OfType<NamespaceDeclarationSyntax>().First().Name.ToString();

            return (typeName, typeNamespace, declarationType);
        }
    }
}
