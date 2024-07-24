using Microsoft.CodeAnalysis;

namespace GameSdk.SourcesGenerators
{

    [Generator]
    public partial class JsonGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            // Generate code only for current assembly or assembly which dependent on current assebly
            // and only for JsonGeneratorSyntaxReceiver
            if (SourceGeneratorParams.ContainsAssembly(context.Compilation) is false
                || context.SyntaxReceiver is not JsonGeneratorSyntaxReceiver jsonConvertableReceiver)
            {
                return;
            }

            // Generate converters READ
            var convertersRead = GetConvertersReadAll(jsonConvertableReceiver.Converters);

            foreach (var converter in convertersRead)
            {
                context.AddSource(converter.fileName, converter.fileContent);
            }

            // Generate partial types with Key property
            var convertables = GetConvertablesAll(jsonConvertableReceiver.Convertables);

            foreach (var convertable in convertables.files)
            {
                context.AddSource(convertable.fileName, convertable.fileContent);
            }

            // Generate code only for current assembly
            // Optional: All attributes and converter (not caches) can be located inside the assembly code and not created by the generator
            if (SourceGeneratorParams.ContainsAssembly(context.Compilation.AssemblyName) is false)
            {
                return;
            }

            var converterRead = GetConverterRead();
            var converterReadAttribute = GetConverterReadAttribute();
            var convertersReadCache = GetConvertersReadCache(convertersRead);

            var convertableAttribute = GetConvertableAttribute();
            var convertablesCache = GetConvertablesCache(convertables.map);

            context.AddSource(converterRead.fileName, converterRead.fileContent);                   // Optional
            context.AddSource(converterReadAttribute.fileName, converterReadAttribute.fileContent); // Optional
            context.AddSource(convertersReadCache.fileName, convertersReadCache.fileContent);       // Required

            context.AddSource(convertableAttribute.fileName, convertableAttribute.fileContent);     // Optional
            context.AddSource(convertablesCache.fileName, convertablesCache.fileContent);           // Required
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new JsonGeneratorSyntaxReceiver());
        }
    }
}
