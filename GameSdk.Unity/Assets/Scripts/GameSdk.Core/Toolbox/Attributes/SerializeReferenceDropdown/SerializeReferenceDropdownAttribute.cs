using System;
using UnityEngine;

namespace GameSdk.Core.Toolbox
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SerializeReferenceDropdownAttribute : PropertyAttribute
    {
        public Type Type { get; }
        public bool GroupByNamespace { get; }

        public SerializeReferenceDropdownAttribute(Type type, bool groupByNamespace = false)
        {
            Type = type;
            GroupByNamespace = groupByNamespace;
        }
    }
}