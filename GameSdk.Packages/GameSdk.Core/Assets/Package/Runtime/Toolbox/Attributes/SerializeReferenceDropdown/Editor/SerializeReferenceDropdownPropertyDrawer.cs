﻿#if UNITY_EDITOR
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

            if (container.childCount > 0)
            {
                container.style.borderBottomLeftRadius = 2;
                container.style.borderBottomRightRadius = 2;
                container.style.borderTopLeftRadius = 2;
                container.style.borderTopRightRadius = 2;
                container.style.backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.15f);
                container.style.paddingBottom = 5;
                container.style.paddingTop = 5;
                container.style.paddingLeft = 5;
                container.style.paddingRight = 10;

                return container;
            }

            return null;
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

            var labelText = dropdownAttr.Label ?? property.displayName;
            var values = GetDropdownValues(dropdownAttr.GroupByNamespace, dropdownAttr.Types);

            var choices = values.Select(v => v.DisplayName).ToList();
            var typeName = GetTypeName(property.managedReferenceFullTypename);
            var index = values.FindIndex((data) => data.Type != null && data.Type.FullName == typeName);

            var root = new VisualElement();
            var dropdownField = new DropdownField(labelText, choices, Mathf.Max(index, 0));

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

                if (propertyField != null)
                {
                    root.Add(propertyField);
                }
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

        protected virtual List<SerializeReferenceDropdownData> GetDropdownValues(bool group, Type[] types)
        {
            return types.SelectMany(GetAllAssignableTypes)
                .Select(t => new SerializeReferenceDropdownData(t, $"{t.FullName}", group ? t.Namespace : null))
                .Prepend(new SerializeReferenceDropdownData(null, "<null>"))
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
