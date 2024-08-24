using System;
using UnityEngine;

namespace GameSdk.Sources.Core.Toolbox
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SerializeReferenceDropdownAttribute : PropertyAttribute
    {
        public Type[] Types { get; }
        public bool GroupByNamespace { get; }

        public SerializeReferenceDropdownAttribute()
        {
            GroupByNamespace = false;
            Types = Array.Empty<Type>();
        }

        public SerializeReferenceDropdownAttribute(params Type[] types)
        {
            GroupByNamespace = false;
            Types = types;
        }

        public SerializeReferenceDropdownAttribute(bool groupByNamespace, params Type[] types)
        {
            GroupByNamespace = groupByNamespace;
            Types = types;
        }
    }
}
