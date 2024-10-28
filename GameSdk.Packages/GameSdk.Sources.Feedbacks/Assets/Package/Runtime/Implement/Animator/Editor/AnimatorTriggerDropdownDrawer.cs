#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameSdk.Sources.Feedbacks
{
    [CustomPropertyDrawer(typeof(AnimatorTriggerDropdownAttribute), true)]
    public class AnimatorTriggerDropdownDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            if (attribute is not AnimatorTriggerDropdownAttribute dropdownAttr)
            {
                return new Label() { name = "unity-invalid-type-label" };
            }

            var values = GetAnimatorTriggers(property, dropdownAttr.AnimatorFieldName);
            var index = values.FindIndex((data) => data.Equals(property.boxedValue));

            var dropdownField = new DropdownField(property.displayName, values, index);

            dropdownField.RegisterCallback<ChangeEvent<string>>((evt) =>
            {
                var valueIndex = values.FindIndex((e) => e.Equals(evt.newValue));

                property.boxedValue = valueIndex > -1 && values.Count > valueIndex ? values[valueIndex] : null;
                property.serializedObject.ApplyModifiedProperties();
            });

            return dropdownField;
        }

        private List<string> GetAnimatorTriggers(SerializedProperty property, string fieldName)
        {
            if (property.serializedObject.FindProperty(fieldName).objectReferenceValue is not UnityEngine.Animator animator)
            {
                return new List<string>();
            }

            var animatorParameters = animator.parameters;

            var triggers = new List<string>();

            foreach (var parameter in animatorParameters)
            {
                if (parameter.type == AnimatorControllerParameterType.Trigger)
                {
                    triggers.Add(parameter.name);
                }
            }

            return triggers;
        }
    }
}
#endif
