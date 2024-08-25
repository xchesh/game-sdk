#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using GameSdk.Core.Toolbox;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Project.Common.UnityContainer
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
                DrawInstallersList(_listProperty);
            }

            serializedObject.ApplyModifiedProperties();
        }

        private bool DrawInstallersPath(SerializedProperty property)
        {
            bool result = true;

            EditorGUILayout.Space(5);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(property);

            if (DrawButton(new GUIContent("Apply", "Find all installers from path and add to array"),
                    new Color(0, 0.5f, 0.8f, 1f), 80f))
            {
                result = false;
                _self.Update();
            }

            ;

            EditorGUILayout.EndHorizontal();

            return result;
        }

        private void DrawInstallersSelect(SerializedProperty property)
        {
            EditorGUILayout.Space();

            var rect = EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(property.displayName);

            if (EditorGUILayout.DropdownButton(new GUIContent("Select IUnityInstaller", "Select and add installers"),
                    FocusType.Keyboard))
            {
                var dropdownItems = GetDropdownItems();
                var dropdown = new UnityInstallersDropdown("IUnityInstaller", dropdownItems,
                    new AdvancedDropdownState(), property);
                int maxGroupSize = dropdownItems.GroupBy(i => i.Group).Max(g => g.Count());
                int maxItems = Mathf.Max(maxGroupSize + 2, 7);

                dropdown.SetMinimumSize(rect.width, EditorGUIUtility.singleLineHeight * maxItems + 5);
                dropdown.Show(rect);
            }

            EditorGUILayout.EndHorizontal();
        }

        private void DrawInstallersList(SerializedProperty property)
        {
            if (property.arraySize < 1)
            {
                EditorGUILayout.HelpBox(
                    "Installers not found. Please select an installer or apply all installers from the specified path.",
                    MessageType.None);

                return;
            }

            for (int i = 0; i < property.arraySize; i++)
            {
                bool breaking = false;

                EditorGUILayout.BeginHorizontal();
                // Disable for prevent changing
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.PropertyField(property.GetArrayElementAtIndex(i), GUIContent.none);
                EditorGUI.EndDisabledGroup();

                if (DrawButton(new GUIContent("Delete", "Delete installer"), Color.red, 80))
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
