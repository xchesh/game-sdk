using System;

namespace GameSdk.Core.Toolbox
{
    public class SerializedTypeDropdownData : AdvancedDropdownData
    {
        public Type Type { get; }

        public SerializedTypeDropdownData(Type value, string displayName, string group = null) : base(value, displayName, group)
        {
            Type = value;
        }
    }
}
