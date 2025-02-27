using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameSdk.Core.Toolbox
{
    public static class EditorExtensions
    {
        public static IEnumerable<T> GetAssets<T>() where T : Object
        {
#if UNITY_EDITOR
            var assets = UnityEditor.AssetDatabase.FindAssets(" t: " + typeof(T).Name);

            return assets.Select(UnityEditor.AssetDatabase.GUIDToAssetPath)
                .Select(UnityEditor.AssetDatabase.LoadAssetAtPath<T>);
#endif
            return Enumerable.Empty<T>();
        }

        public static T GetAsset<T>() where T : Object
        {
#if UNITY_EDITOR
            var assets = GetAssets<T>().ToArray();

            if (assets.Any())
            {
                return assets.First();
            }
#endif
            return default;
        }
    }
}
