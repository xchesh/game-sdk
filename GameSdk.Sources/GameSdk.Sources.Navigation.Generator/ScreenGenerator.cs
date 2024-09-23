using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GameSdk.Sources.Navigation
{
    [Generator]
    public class ScreenGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            // Generate code only for current assembly or assembly which dependent on current assebly
            // and only for JsonNotifySyntaxReceiver
            if (SourceGeneratorParams.ContainsAssembly(context.Compilation) is false
                || context.SyntaxReceiver is not ScreenSyntaxReceiver screenSyntaxReceiver)
            {
                return;
            }

            // Generate partial screens with generated properties
            var screens = GetScreenAll(screenSyntaxReceiver.Candidates);

            foreach (var screen in screens)
            {
                context.AddSource(screen.fileName, screen.fileContent);
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new ScreenSyntaxReceiver());
        }

        private List<(string fileName, string fileContent)> GetScreenAll(List<TypeDeclarationSyntax> candidates)
        {
            var convertablesList = new List<(string, string)>();

            foreach (var candidate in candidates)
            {
                var (fileName, fileContent, fullName) = GetScreen(candidate);

                convertablesList.Add((fileName, fileContent));
            }

            return convertablesList;
        }

        private (string fileName, string fileContent, string fullName) GetScreen(TypeDeclarationSyntax typeDeclarationSyntax)
        {
            var (typeName, typeNamespace, declarationType) = typeDeclarationSyntax.GetTypeDeclaration();

            var modifiers = string.Join(" ", typeDeclarationSyntax.Modifiers); // Save original modifiers
            var fullName = $"global::{typeNamespace}.{typeName}";
            var fileName = $"{typeName}.g.cs";
            var fileContent = $@"
using System;
using UnityEngine.UIElements;
using {ScreenParams.nameNamespace};

namespace {typeNamespace}
{{
    {modifiers} {declarationType} {typeName} : IScreen
    {{
        public INavigation Navigation {{ get; private set; }}
        public IScreenConfig Config {{ get; private set; }}
        public VisualElement VisualElement {{ get; private set; }}

        public bool IsActive {{ get; private set; }}
        public bool IsVisible {{ get; private set; }}

        void IScreen.SetConfig(IScreenConfig config)
        {{
            Config = config;
        }}

        void IScreen.SetIsActive(bool isActive)
        {{
            IsActive = isActive;
        }}

        void IScreen.SetIsVisible(bool isVisible)
        {{
            IsVisible = isVisible;
        }}

        void IScreen.SetNavigation(INavigation navigation)
        {{
            Navigation = navigation;
        }}

        void IScreen.SetVisualElement(VisualElement visualElement)
        {{
            VisualElement = visualElement;
        }}
    }}
}}
";
            return (fileName, fileContent, fullName);
        }
    }
}
