#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using GameSdk.Core.Toolbox;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace GameSdk.UnityContainer
{
    [CustomEditor(typeof(UnityInstaller))]
    internal class UnityInstallerEditor : Editor
    {
        private SerializedProperty _listProperty;
        private SerializedProperty _pathProperty;
        private UnityInstaller _self;

        private void OnEnable()
        {
            _listProperty = serializedObject.FindProperty("_installers");
            _pathProperty = serializedObject.FindProperty("_path");

            _self = serializedObject.targetObject as UnityInstaller;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawDefaultInspector();

            if (DrawInstallersPath(_pathProperty))
            {
                DrawInstallersSelect(_listProperty);
                DrawInstallersSearch();
                DrawInstallersList(_listProperty);
            }

            serializedObject.ApplyModifiedProperties();
        }

        private bool DrawInstallersPath(SerializedProperty property)
        {
            bool result = true;

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(property);

            if (DrawButton(new GUIContent("Apply", "Find all installers from path and add to array"), new Color(0, 0.5f, 0.8f, 1f), 80f))
            {
                result = false;
                _self.Update();
            }

            EditorGUILayout.EndHorizontal();

            return result;
        }

        private void DrawInstallersSelect(SerializedProperty property)
        {
            var rect = EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(property.displayName);

            if (EditorGUILayout.DropdownButton(new GUIContent("Select IUnityInstaller", "Select and add installers"), FocusType.Keyboard))
            {
                var dropdownItems = GetDropdownItems();
                var dropdown = new UnityInstallersDropdown(
                    "IUnityInstaller",
                    dropdownItems,
                    new AdvancedDropdownState(),
                    property
                );

                var maxGroupSize = 5;

                if (dropdownItems.Count > 0)
                {
                    maxGroupSize = dropdownItems.GroupBy(i => i.Group).Max(g => g.Count());
                }

                var maxItems = Mathf.Max(maxGroupSize + 2, 7);

                dropdown.SetMinimumSize(rect.width, EditorGUIUtility.singleLineHeight * maxItems + 5);
                dropdown.Show(rect);
            }

            EditorGUILayout.EndHorizontal();
        }

        private void DrawInstallersSearch()
        {
            EditorGUILayout.Space();

            var search = EditorGUILayout.TextField("Search", _self.Search);

            if (search != _self.Search)
            {
                _self.Search = search;
            }
        }

        private void DrawInstallersList(SerializedProperty property)
        {
            if (property.arraySize < 1)
            {
                EditorGUILayout.HelpBox(
                    "Installers not found. Please select an installer or apply all installers from the specified path.",
                    MessageType.None
                );

                return;
            }

            var availableIndexes = _self.GetInstallersIndexes();

            if (availableIndexes.Count < 1)
            {
                EditorGUILayout.HelpBox("Installers not found. Change search or apply all installers from the specified path.", MessageType.None);
            }

            for (int i = 0; i < property.arraySize; i++)
            {
                bool available = availableIndexes.Contains(i);
                bool breaking = false;

                if (available is false)
                {
                    continue;
                }

                EditorGUILayout.BeginHorizontal();
                // Disable for prevent changing
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.PropertyField(property.GetArrayElementAtIndex(i), GUIContent.none);
                EditorGUI.EndDisabledGroup();

                if (DrawButton(new GUIContent("Reset", "Reset installer"), Color.yellow, 60))
                {
                    _self.Reset(i);
                }

                if (DrawButton(new GUIContent("Delete", "Delete installer"), Color.red, 60))
                {
                    breaking = true;

                    _self.Remove(i);
                }

                EditorGUILayout.EndHorizontal();

                if (breaking)
                {
                    break;
                }
            }
        }

        private bool DrawButton(GUIContent content, Color color, float witdh)
        {
            bool result = false;

            var backgroundColor = GUI.backgroundColor;
            GUI.backgroundColor = color;

            if (GUILayout.Button(content, GUILayout.Width(witdh)))
            {
                result = true;
            }

            GUI.backgroundColor = backgroundColor;

            return result;
        }

        private List<AdvancedDropdownData> GetDropdownItems()
        {
            return _self.GetAllInstallers(null).Select(i => new AdvancedDropdownData(i, i.Name, i.Namespace)).ToList();
        }
    }
}
#endif
