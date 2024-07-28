using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Games.Uni.Common.SourcesGenerators
{
    internal class JsonNotifySyntaxReceiver : ISyntaxReceiver
    {
        public List<TypeDeclarationSyntax> Candidates { get; } = new List<TypeDeclarationSyntax>();
        public List<FieldDeclarationSyntax> CandidateFields { get; } = new List<FieldDeclarationSyntax>();
        public List<IFieldSymbol> Fields { get; } = new List<IFieldSymbol>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is TypeDeclarationSyntax typeDeclarationSyntax)
            {
                if (typeDeclarationSyntax.AttributeLists.ContainsAttribute(JsonNotifyParams.nameClassAttribute))
                {
                    Candidates.Add(typeDeclarationSyntax);
                }
            }

            if (syntaxNode is FieldDeclarationSyntax fieldDeclarationSyntax)
            {
                if (fieldDeclarationSyntax.AttributeLists.ContainsAttribute(JsonNotifyParams.nameFieldAttribute))
                {
                    CandidateFields.Add(fieldDeclarationSyntax);
                }
            }
        }
    }
}
