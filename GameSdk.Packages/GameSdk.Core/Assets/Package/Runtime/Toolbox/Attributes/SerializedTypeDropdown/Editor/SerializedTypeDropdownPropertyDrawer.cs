#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Linq;
using GameSdk.Core.Essentials;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameSdk.Core.Toolbox
{
    [CustomPropertyDrawer(typeof(SerializedTypeDropdownAttribute), true)]
    public class SerializedTypeDropdownPropertyDrawer : PropertyDrawer
    {
        private static Dictionary<Type, IEnumerable<Type>> _cachedTypes = new();

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            if (property.propertyType == SerializedPropertyType.ManagedReference)
            {
                return new PropertyField(property);
            }

            if (attribute is not SerializedTypeDropdownAttribute dropdownAttr)
            {
                return new Label() { name = "unity-invalid-type-label" };
            }

            var values = GetDropdownValues(dropdownAttr.Types);

            var choices = values.Select(v => v.DisplayName).ToList();
            var typeFullName = "";

            if (property.boxedValue is SerializedType serializedType)
            {
                typeFullName = serializedType.ClassName;
            }

            var index = values.FindIndex((data) => data.Type != null && data.Type.FullName == typeFullName);

            var root = new VisualElement();
            var dropdownField = new DropdownField(property.displayName, choices, Mathf.Max(index, 0));

            dropdownField.RegisterCallback<ChangeEvent<string>>((evt) =>
            {
                var valueIndex = choices.FindIndex((e) => e.Equals(evt.newValue));
                var type = valueIndex > -1 && values.Count > valueIndex ? values[valueIndex].Type : null;

                property.boxedValue = new SerializedType { Value = type };
                property.serializedObject.ApplyModifiedProperties();
            });

            dropdownField.AddToClassList("unity-base-field__aligned");

            root.Add(dropdownField);

            return root;
        }

        protected virtual string GetTypeName(string typeName)
        {
            var paths = typeName.Split(' ');

            if (paths.Length > 1)
            {
                return paths[1];
            }

            return typeName;
        }

        protected virtual List<SerializedTypeDropdownData> GetDropdownValues(Type[] types)
        {
            return types.SelectMany(GetAllAssignableTypes)
                .Select(t => new SerializedTypeDropdownData(t, $"{t.FullName}", t.Namespace))
                .Prepend(new SerializedTypeDropdownData(null, "<null>"))
                .ToList();
        }

        private IEnumerable<Type> GetAllAssignableTypes(Type type)
        {
            if (_cachedTypes.ContainsKey(type) is false)
            {
                _cachedTypes.Add(type, type.GetAssignableTypes());
            }

            return _cachedTypes[type];
        }

        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptsReloaded()
        {
            _cachedTypes = new Dictionary<Type, IEnumerable<Type>>();
        }
    }
}

#endif
