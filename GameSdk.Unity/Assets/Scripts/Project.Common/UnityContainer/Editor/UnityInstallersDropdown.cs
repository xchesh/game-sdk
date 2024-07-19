#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using GameSdk.Core.Toolbox;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace Project.Common.UnityContainer
{
    internal class UnityInstallersDropdown : AdvancedDropdownBase
    {
        public UnityInstallersDropdown(string name, IEnumerable<AdvancedDropdownData> items,
            AdvancedDropdownState state, SerializedProperty property) : base(name, items, state, property)
        {
        }

        protected override void SetPropertyValue(AdvancedDropdownBaseItem item)
        {
            if (Property.serializedObject.targetObject is UnityInstaller unityInstaller)
            {
                unityInstaller.Add(item.Value as Type);

                AssetDatabase.SaveAssetIfDirty(unityInstaller);
            }
        }
    }
}
#endif
