using System;
using System.Collections.Generic;
using System.Linq;

namespace GameSdk.Core.Toolbox
{
    public static class TypeExtensions
    {
        public static IEnumerable<Type> GetAllTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes());
        }

        public static IEnumerable<Type> GetAssignableTypes(this Type type)
        {
            return GetAllTypes().Where(p => type.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract);
        }

        public static bool IsNullable(this Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }
    }
}
