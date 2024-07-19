using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GameSdk.SourcesGenerators
{
    public class JsonGeneratorSyntaxReceiver : ISyntaxReceiver
    {
        public List<TypeDeclarationSyntax> Convertables { get; } = new List<TypeDeclarationSyntax>();
        public List<TypeDeclarationSyntax> Converters { get; } = new List<TypeDeclarationSyntax>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is TypeDeclarationSyntax typeDeclarationSyntax)
            {
                var attributes = typeDeclarationSyntax.AttributeLists.SelectMany(a => a.Attributes);

                if (attributes.Any(a => JsonGeneratorParams.ContainsAttribute(a.Name.ToString(), JsonGeneratorParams.nameConvertable)))
                {
                    Convertables.Add(typeDeclarationSyntax);
                }

                if (attributes.Any(a => JsonGeneratorParams.ContainsAttribute(a.Name.ToString(), JsonGeneratorParams.nameConverterRead)))
                {
                    Converters.Add(typeDeclarationSyntax);
                }
            }
        }
    }
}
