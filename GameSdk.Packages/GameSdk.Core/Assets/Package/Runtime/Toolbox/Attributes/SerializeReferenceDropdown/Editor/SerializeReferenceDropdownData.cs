using System;

namespace GameSdk.Core.Toolbox
{
    public class SerializeReferenceDropdownData : AdvancedDropdownData
    {
        public Type Type { get; }

        public SerializeReferenceDropdownData(Type value, string displayName, string group = null) : base(value, displayName, group)
        {
            Type = value;
        }
    }
}
