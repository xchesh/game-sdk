#if UNITY_EDITOR
using UnityEditor.IMGUI.Controls;

namespace GameSdk.Sources.Core.Toolbox
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
#endif
