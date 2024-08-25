#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GameSdk.Sources.Services.GraphicQuality
{
    [CustomPropertyDrawer(typeof(GraphicQualityAttribute))]
    public class GraphicQualityPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            int qualityCount = QualitySettings.count;

            string[] enumNames = new string[qualityCount];
            for (int i = 0; i < qualityCount; i++)
            {
                enumNames[i] = QualitySettings.names[i];
            }

            int selectedIndex = property.intValue;

            selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, enumNames);

            property.intValue = selectedIndex;

            EditorGUI.EndProperty();
        }
    }
}
#endif
