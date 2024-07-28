using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GameSdk.Sources.Json
{
    public class JsonGeneratorSyntaxReceiver : ISyntaxReceiver
    {
        public List<TypeDeclarationSyntax> Convertables { get; } = new List<TypeDeclarationSyntax>();
        public List<TypeDeclarationSyntax> Converters { get; } = new List<TypeDeclarationSyntax>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is TypeDeclarationSyntax typeDeclarationSyntax)
            {
                if (typeDeclarationSyntax.AttributeLists.ContainsAttribute(JsonGeneratorParams.nameConvertable))
                {
                    Convertables.Add(typeDeclarationSyntax);
                }

                if (typeDeclarationSyntax.AttributeLists.ContainsAttribute(JsonGeneratorParams.nameConverterRead))
                {
                    Converters.Add(typeDeclarationSyntax);
                }
            }
        }
    }
}
