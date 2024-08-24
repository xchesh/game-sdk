using System.Collections.Generic;
using System.Linq;

namespace GameSdk.Sources.Core.Common
{
    public static class ParametersExtensions
    {
        public static bool Contains<T>(this IEnumerable<T> enumerable) where T : IParameter
        {
            return enumerable.Any();
        }

        public static bool TryGetParameter<T>(this IEnumerable<IParameter> enumerable, out T parameter) where T : IParameter
        {
            parameter = enumerable.OfType<T>().FirstOrDefault();

            return parameter != null;
        }
    }
}
