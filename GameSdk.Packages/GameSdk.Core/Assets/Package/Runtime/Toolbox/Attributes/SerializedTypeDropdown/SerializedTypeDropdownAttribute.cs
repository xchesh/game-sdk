using System;
using UnityEngine;

namespace GameSdk.Core.Toolbox
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SerializedTypeDropdownAttribute : PropertyAttribute
    {
        public Type[] Types { get; }

        public SerializedTypeDropdownAttribute()
        {
            Types = Array.Empty<Type>();
        }

        public SerializedTypeDropdownAttribute(params Type[] types)
        {
            Types = types;
        }
    }
}
