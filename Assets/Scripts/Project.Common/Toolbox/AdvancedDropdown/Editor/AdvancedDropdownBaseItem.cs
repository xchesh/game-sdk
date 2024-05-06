using UnityEditor.IMGUI.Controls;

namespace Core.Common.Toolbox
{
    public class AdvancedDropdownBaseItem : AdvancedDropdownItem
    {
        public object Value { get; }

        public AdvancedDropdownBaseItem(object value, string name) : base(name)
        {
            Value = value;
        }
    }
}
