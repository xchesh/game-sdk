using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameSdk.Sources.Feedbacks.Generator
{
    [Generator]
    public class FeedbackGenerator : ISourceGenerator
    {

        public void Execute(GeneratorExecutionContext context)
        {
            if (SourceGeneratorParams.ContainsAssembly(context.Compilation) is false
                || context.SyntaxReceiver is not FeedbackGeneratorSyntaxReceiver receiver
                || SourceGeneratorParams.ContainsAssembly(context.Compilation.AssemblyName) is false)
            {
                return;
            }

            var feedbacksStrategies = GetFeedbacksStrategyNames(receiver.CandidateClasses);
            var feedbacksStrategiesCache = GetFeedbackStrategyCache(feedbacksStrategies);

            context.AddSource(feedbacksStrategiesCache.fileName, feedbacksStrategiesCache.fileContent);
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new FeedbackGeneratorSyntaxReceiver());
        }

        private (string fileName, string fileContent) GetFeedbackStrategyCache(IEnumerable<string> strategies)
        {
            var fileName = $"{FeedbackGeneratorParams.nameFeedbackStrategy}Cache.g.cs";
            var fileContent = new StringBuilder($@"
using System;
using System.Collections.Generic;

namespace {FeedbackGeneratorParams.nameNamespace}
{{
    public sealed class {FeedbackGeneratorParams.nameFeedbackStrategy}Cache
    {{
        public static readonly IReadOnlyDictionary<Type, I{FeedbackGeneratorParams.nameFeedbackStrategy}> Strategies = new Dictionary<Type, I{FeedbackGeneratorParams.nameFeedbackStrategy}>()
        {{
");
            foreach (var strategy in strategies)
            {
                fileContent.AppendLine($@"            {{ typeof({strategy}), new {strategy}() }}, ");
            }

            fileContent.AppendLine($@"
        }};
    }}
}}
");
            return (fileName, fileContent.ToString());
        }

        private string GetFeedbackStrategyName(TypeDeclarationSyntax typeDeclarationSyntax)
        {
            var (typeName, typeNamespace, declarationType) = typeDeclarationSyntax.GetTypeDeclaration();

            var name = FeedbackGeneratorParams.nameFeedbackStrategy + typeName;
            return $"global::{typeNamespace}.{typeName}";
        }

        private IEnumerable<string> GetFeedbacksStrategyNames(List<TypeDeclarationSyntax> candidates)
        {
            return candidates.Select(GetFeedbackStrategyName);
        }
    }

}
