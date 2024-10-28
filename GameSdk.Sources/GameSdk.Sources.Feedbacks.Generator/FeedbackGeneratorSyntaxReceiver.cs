using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;

namespace GameSdk.Sources.Feedbacks.Generator
{
    internal class FeedbackGeneratorSyntaxReceiver : ISyntaxReceiver
    {
        public List<TypeDeclarationSyntax> CandidateClasses { get; } = new List<TypeDeclarationSyntax>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is TypeDeclarationSyntax typeDeclarationSyntax)
            {
                if (typeDeclarationSyntax.AttributeLists.ContainsAttribute(FeedbackGeneratorParams.nameFeedbackStrategy))
                {
                    CandidateClasses.Add(typeDeclarationSyntax);
                }
            }
        }
    }
}
