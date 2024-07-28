using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GameSdk.Sources.Json
{
    [Generator]
    internal class JsonNotifyGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            // Generate code only for current assembly or assembly which dependent on current assebly
            // and only for JsonNotifySyntaxReceiver
            if (SourceGeneratorParams.ContainsAssembly(context.Compilation) is false
                || context.SyntaxReceiver is not JsonNotifySyntaxReceiver notifyPropertyChangedSyntaxReceiver)
            {
                return;
            }

            foreach (var candidate in notifyPropertyChangedSyntaxReceiver.Candidates)
            {
                var sourceModel = context.Compilation.GetSemanticModel(candidate.SyntaxTree);
                var sourceSymbol = sourceModel.GetDeclaredSymbol(candidate);

                if (sourceSymbol is ITypeSymbol typeSymbol)
                {
                    var members = typeSymbol.GetMembers().Where(m => !m.IsImplicitlyDeclared && !m.IsStatic);
                    var fields = members.Where(m => m.Kind == SymbolKind.Field && !m.IsAbstract && m.IsDefinition)
                        .Cast<IFieldSymbol>()
                        .Where(f => !f.IsConst && !f.IsReadOnly && !f.IsVolatile)
                        .Where(f => f.GetAttributes().ContainsAttribute(JsonNotifyParams.nameFieldAttribute));

                    var result = GetCandidate(candidate, fields);

                    context.AddSource(result.fileName, result.fileContent);
                }
            }

            // Generate code only for current assembly
            // Optional: All attributes can be located inside the assembly code and not created by the generator
            if (SourceGeneratorParams.ContainsAssembly(context.Compilation.AssemblyName) is false)
            {
                return;
            }

            var notifyPropertyChangedAttribute = GetAttribute();

            context.AddSource(notifyPropertyChangedAttribute.fileName, notifyPropertyChangedAttribute.fileContent);
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new JsonNotifySyntaxReceiver());

        }

        private (string fileName, string fileContent) GetAttribute()
        {
            var fileName = $"{JsonNotifyParams.nameClassAttribute}Attribute.g.cs";
            var fileContent = $@"
using System;

namespace {JsonNotifyParams.nameNamespace}
{{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
    public sealed class {JsonNotifyParams.nameClassAttribute}Attribute : Attribute
    {{
        public {JsonNotifyParams.nameClassAttribute}Attribute()
        {{
        }}
    }}
}}
";
            return (fileName, fileContent);
        }

        // Generate single partial candidate type
        private (string fileName, string fileContent) GetCandidate(TypeDeclarationSyntax typeDeclarationSyntax, IEnumerable<IFieldSymbol> fields)
        {
            var properties = GetProperties(fields);

            var (typeName, typeNamespace, declarationType) = typeDeclarationSyntax.GetTypeDeclaration();

            var modifiers = string.Join(" ", typeDeclarationSyntax.Modifiers); // Save original modifiers
            var fileName = $"{typeName}.g.cs";
            var fileContent = $@"
using System;
using System.ComponentModel;

namespace {typeNamespace}
{{
    {modifiers} {declarationType} {typeName} : INotifyPropertyChanged
    {{
        {properties}

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {{
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }}
    }}
}}
";
            return (fileName, fileContent);
        }


        /// <summary>
        /// Generates the properties for the given fields.
        /// </summary>
        /// <param name="fields">The fields to generate properties for.</param>
        /// <returns>A string containing the generated properties.</returns>
        private string GetProperties(IEnumerable<IFieldSymbol> fields)
        {
            var stringBuilder = new StringBuilder();

            foreach (var field in fields)
            {
                var propertyName = field.Name.ToSafePropertyName();

                stringBuilder.AppendLine($@"
        public {field.Type} {propertyName}
        {{
            get => {field.Name};
            set
            {{
                if ({field.Name} == value) return;

                {field.Name} = value;
                OnPropertyChanged(""{propertyName}"");
            }}
        }}");
            }

            return stringBuilder.ToString();
        }
    }
}
