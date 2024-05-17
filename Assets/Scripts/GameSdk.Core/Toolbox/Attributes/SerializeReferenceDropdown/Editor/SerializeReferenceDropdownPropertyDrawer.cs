using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameSdk.Core.Toolbox
{
    [CustomPropertyDrawer(typeof(SerializeReferenceDropdownAttribute), true)]
    public class SerializeReferenceDropdownPropertyDrawer : PropertyDrawer
    {
        private static Dictionary<Type, IEnumerable<Type>> _cachedTypes = new();

        private VisualElement CreateInspectorGUI(SerializedProperty property)
        {
            var container = new VisualElement();

            container.style.borderBottomLeftRadius = 2;
            container.style.borderBottomRightRadius = 2;
            container.style.borderTopLeftRadius = 2;
            container.style.borderTopRightRadius = 2;
            container.style.backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.15f);
            container.style.paddingBottom = 5;
            container.style.paddingTop = 5;
            container.style.paddingLeft = 5;
            container.style.paddingRight = 10;

            var propertyCopy = property.Copy();
            var propertyPath = property.propertyPath + ".";

            if (propertyCopy.NextVisible(true))
            {
                do
                {
                    if (propertyCopy.propertyPath.StartsWith(propertyPath) is false || propertyCopy.depth <= property.depth)
                    {
                        break;
                    }
                    
                    container.Add(new PropertyField(propertyCopy));
                } while (propertyCopy.NextVisible(false));
            }

            return container;
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            if (property.propertyType != SerializedPropertyType.ManagedReference)
            {
                return new PropertyField(property);
            }

            if (attribute is not SerializeReferenceDropdownAttribute dropdownAttr)
            {
                return new Label() { name = "unity-invalid-type-label" };
            }

            var values = GetDropdownValues(dropdownAttr.Type, dropdownAttr.GroupByNamespace);

            var choices = values.Select(v => v.DisplayName).ToList();
            var typeName = GetTypeName(property.managedReferenceFullTypename);
            var index = values.FindIndex((data) => data.Type != null && data.Type.FullName == typeName);

            var root = new VisualElement();
            var dropdownField = new DropdownField(property.displayName, choices, Mathf.Max(index, 0));

            dropdownField.RegisterCallback<ChangeEvent<string>>((evt) =>
            {
                var valueIndex = choices.FindIndex((e) => e.Equals(evt.newValue));
                var type = valueIndex > -1 && values.Count > valueIndex ? values[valueIndex].Type : null;

                property.managedReferenceValue = null;

                if (type is not null)
                {
                    property.managedReferenceValue = Activator.CreateInstance(type);
                }

                property.serializedObject.ApplyModifiedProperties();
            });

            root.Add(dropdownField);

            if (property.managedReferenceValue != null)
            {
                var propertyField = CreateInspectorGUI(property);
                root.Add(propertyField);
            }

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

        protected virtual List<SerializeReferenceDropdownData> GetDropdownValues(Type type, bool group)
        {
            if (_cachedTypes.ContainsKey(type) is false)
            {
                _cachedTypes.Add(type, type.GetAssignableTypes());
            }

            return _cachedTypes[type]
                .Select(t =>
                    new SerializeReferenceDropdownData(t, $"{t.Name} : {type.Name}", group ? t.Namespace : null))
                .Prepend(new SerializeReferenceDropdownData(null, "<null>"))
                .ToList();
        }

        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptsReloaded()
        {
            _cachedTypes = new Dictionary<Type, IEnumerable<Type>>();
        }
    }
}