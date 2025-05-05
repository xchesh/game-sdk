using System;
using UnityEngine;

namespace GameSdk.Core.Toolbox
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SerializeReferenceDropdownAttribute : PropertyAttribute
    {
        public string Label { get; }
        public bool GroupByNamespace { get; }
        public Type[] Types { get; }

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

        public SerializeReferenceDropdownAttribute(string label, bool groupByNamespace, params Type[] types)
        {
            Label = label;
            GroupByNamespace = groupByNamespace;
            Types = types;
        }
    }
}
