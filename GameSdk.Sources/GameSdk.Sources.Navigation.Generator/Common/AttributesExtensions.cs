using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GameSdk.Sources.Navigation
{
    /// <summary>
    /// Extension methods for working with attributes.
    /// </summary>
    internal static class AttributesExtensions
    {
        /// <summary>
        /// Checks if the syntax list of attribute lists contains the specified attribute name.
        /// </summary>
        /// <param name="attributes">The syntax list of attribute lists.</param>
        /// <param name="attributeName">The name of the attribute to check.</param>
        /// <returns><c>true</c> if the attribute name is found; otherwise, <c>false</c>.</returns>
        internal static bool ContainsAttribute(this SyntaxList<AttributeListSyntax> attributes, string attributeName)
        {
            return string.IsNullOrEmpty(attributeName) is false && attributes.SelectMany(a => a.Attributes).Any(a => SourceGeneratorParams.ContainsAttribute(a.Name.ToString(), attributeName));
        }

        /// <summary>
        /// Checks if the enumerable of attribute data contains the specified attribute name.
        /// </summary>
        /// <param name="attributes">The enumerable of attribute data.</param>
        /// <param name="attributeName">The name of the attribute to check.</param>
        /// <returns><c>true</c> if the attribute name is found; otherwise, <c>false</c>.</returns>
        internal static bool ContainsAttribute(this IEnumerable<AttributeData> attributes, string attributeName)
        {
            return string.IsNullOrEmpty(attributeName) is false && attributes.Any(a => SourceGeneratorParams.ContainsAttribute(a.AttributeClass.Name, attributeName));
        }
    }
}
