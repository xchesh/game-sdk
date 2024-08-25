#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using VContainer.Unity;

namespace GameSdk.UnityContainer.VContainer
{
    [InitializeOnLoad]
    public class VContainerScopeHierarchyManager
    {
        private static readonly Color Color = new Color(0.5f, 1f, 0f, 1);

        private static GUIStyle _guiStyle;

        private static GUIStyle GUIStyle => _guiStyle ??= new GUIStyle(EditorStyles.boldLabel)
        {
            normal = { textColor = Color },
            focused = { textColor = Color },
            hover = { textColor = Color },
            active = { textColor = Color },
        };

        static VContainerScopeHierarchyManager()
        {
            EditorApplication.hierarchyWindowItemOnGUI -= HierarchyHighlight_OnGUI;
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyHighlight_OnGUI;
        }

        private static void HierarchyHighlight_OnGUI(int instanceID, Rect selectionRect)
        {
            var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

            if (gameObject != null)
            {
                var component = gameObject.GetComponent<LifetimeScope>();

                if (component != null && Event.current.type == EventType.Repaint)
                {
                    var icon = EditorGUIUtility.IconContent("d_ParticleSystemForceField Icon");

                    if (icon != null)
                    {
                        GUI.DrawTexture(new Rect(selectionRect.xMax - 16, selectionRect.yMin, 16, 16), icon.image);
                    }
                }
            }
        }
    }
}
#endif
