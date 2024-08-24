#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;

namespace GameSdk.Sources.Core.Toolbox
{
    [CustomPropertyDrawer(typeof(DropdownAttribute), true)]
    public class DropdownPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            if (attribute is not DropdownAttribute dropdownAttr)
            {
                return new Label() { name = "unity-invalid-type-label" };
            }

            var values = dropdownAttr.GetDropdownValues().ToList();
            var choices = values.Select(v => v.DisplayName).ToList();
            var index = values.FindIndex((data) => data.Value.Equals(property.boxedValue));

            var dropdownField = new DropdownField(property.displayName, choices, index);

            dropdownField.RegisterCallback<ChangeEvent<string>>((evt) =>
            {
                var valueIndex = choices.FindIndex((e) => e.Equals(evt.newValue));

                property.boxedValue = valueIndex > -1 && values.Count > valueIndex ? values[valueIndex].Value : null;
                property.serializedObject.ApplyModifiedProperties();
            });

            return dropdownField;
        }
    }
}
#endif
