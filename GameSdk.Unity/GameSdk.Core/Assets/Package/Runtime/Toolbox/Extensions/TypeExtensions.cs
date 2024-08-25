using System;
using System.Collections.Generic;
using System.Linq;

namespace GameSdk.Core.Toolbox
{
    public static class TypeExtensions
    {
        public static IEnumerable<Type> GetAssignableTypes(this Type type)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsAbstract);
        }
    }
}
