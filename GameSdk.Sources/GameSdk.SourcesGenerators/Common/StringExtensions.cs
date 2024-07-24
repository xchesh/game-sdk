using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GameSdk.SourcesGenerators
{
    internal static class StringExtensions
    {
        /// <summary>
        /// Converts a string to PascalCase.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <returns>If the string is not null or empty, a string converted to PascalCase; otherwise, the original string value.</returns>
        internal static string ToPascalCase(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var sb = new StringBuilder(value);
                sb[0] = char.ToUpperInvariant(sb[0]);

                return sb.ToString();
            }

            return value;
        }

        /// <summary>
        /// Converts a string to a safe property name.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <returns>A string converted to a safe property name. If the string is already a safe property name, returns the original string value with suffix "Property".</returns>
        internal static string ToSafePropertyName(this string value)
        {
            var propName = value.Replace("m_", "").Replace("_", "").ToPascalCase();

            if (propName == value)
            {
                propName = "Property";
            }

            return propName;
        }
    }
}
