using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GameSdk.Sources.Json
{
    /// <summary>
    /// Provides extension methods for <see cref="TypeDeclarationSyntax"/>.
    /// </summary>
    internal static class TypeDeclarationSyntaxExtensions
    {
        /// <summary>
        /// Gets the declaration type of the <see cref="TypeDeclarationSyntax"/>.
        /// </summary>
        /// <param name="typeDeclarationSyntax">The <see cref="TypeDeclarationSyntax"/> instance.</param>
        /// <returns>The declaration type as a string.</returns>
        internal static string GetDeclarationType(this TypeDeclarationSyntax typeDeclarationSyntax)
        {
            return typeDeclarationSyntax switch
            {
                ClassDeclarationSyntax => "class",
                InterfaceDeclarationSyntax => "interface",
                StructDeclarationSyntax => "struct",
                _ => "record"
            };
        }

        /// <summary>
        /// Gets the type declaration information of the <see cref="TypeDeclarationSyntax"/>.
        /// </summary>
        /// <param name="typeDeclarationSyntax">The <see cref="TypeDeclarationSyntax"/> instance.</param>
        /// <returns>A tuple containing the type name, type namespace, and declaration type.</returns>
        internal static (string typeName, string typeNamespace, string declarationType) GetTypeDeclaration(this TypeDeclarationSyntax typeDeclarationSyntax)
        {
            var declarationType = typeDeclarationSyntax.GetDeclarationType();
            var typeName = typeDeclarationSyntax.Identifier.Text;
            var typeNamespace = typeDeclarationSyntax.Ancestors().OfType<NamespaceDeclarationSyntax>().First().Name.ToString();

            return (typeName, typeNamespace, declarationType);
        }
    }
}
