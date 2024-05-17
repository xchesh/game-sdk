#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace GameSdk.Core.Toolbox
{
    public class AdvancedDropdownBase : AdvancedDropdownAbstract<AdvancedDropdownBaseItem>
    {
        protected readonly SerializedProperty Property;

        public AdvancedDropdownBase(string name, IEnumerable<AdvancedDropdownData> items, AdvancedDropdownState state,
            SerializedProperty property) : base(name, items, state)
        {
            Property = property;
        }

        protected override AdvancedDropdownBaseItem CreateDropdownItem(AdvancedDropdownData data)
        {
            return new AdvancedDropdownBaseItem(data.Value, data.DisplayName);
        }

        protected override void SetPropertyValue(AdvancedDropdownBaseItem item)
        {
            Property.boxedValue = item.Value;
            Property.serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif
