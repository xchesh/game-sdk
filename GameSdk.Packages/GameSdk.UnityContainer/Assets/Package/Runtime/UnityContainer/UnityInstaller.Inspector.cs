#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GameSdk.UnityContainer
{
    public partial class UnityInstaller
    {
        [SerializeField, HideInInspector] private string _path = "Assets/Scripts/Project.Installers";

        internal string Search { get; set; }

        [ContextMenu("Clear")]
        private void Clear()
        {
            foreach (var type in _installers)
            {
                var so = AssetDatabase.LoadAssetAtPath(AssetDatabase.GetAssetPath(type), typeof(IUnityInstaller));
                AssetDatabase.RemoveObjectFromAsset(so);
            }

            _installers.Clear();

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssetIfDirty(this);
        }

        [ContextMenu("Update")]
        internal void Update()
        {
            var installers = GetAllInstallers();

            foreach (var installer in _installers)
            {
                var so = AssetDatabase.LoadAssetAtPath(AssetDatabase.GetAssetPath(installer), typeof(IUnityInstaller));

                if (installers.Contains(so.GetType()) is false)
                {
                    AssetDatabase.RemoveObjectFromAsset(so);
                }
            }

            foreach (var type in installers)
            {
                if (_installers.Any(i => i.GetType() == type))
                {
                    continue;
                }

                Add(type, false);
            }

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssetIfDirty(this);
        }

        internal List<int> GetInstallersIndexes()
        {
            if (string.IsNullOrEmpty(Search))
            {
                return Enumerable.Range(0, _installers.Count).ToList();
            }

            var result = new List<int>();

            for (var i = 0; i < _installers.Count; i++)
            {
                var installer = _installers[i];
                if (installer.GetType().Name.Contains(Search, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(_installers.IndexOf(installer));
                }
            }

            return result;
        }

        public void Reset()
        {
            _path = "Assets/Scripts/Project.Installers";
        }

        private List<Type> GetAllInstallers()
        {
            if (!System.IO.Directory.Exists(_path))
            {
                return new List<Type>();
            }

            return GetAllInstallers(new[] { _path });
        }

        internal List<Type> GetAllInstallers(string[] paths)
        {
            var types = new List<Type>();

            var guids = AssetDatabase.FindAssets("t:Script", paths);
            var installerType = typeof(IUnityInstaller);

            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var type = AssetDatabase.LoadAssetAtPath<UnityEditor.MonoScript>(path)?.GetClass();

                if (type is { IsAbstract: false } && installerType.IsAssignableFrom(type))
                {
                    types.Add(type);
                }
            }

            return types;
        }

        internal void Add(Type type, bool save = true)
        {
            var so = CreateInstance(type);
            so.name = type.Name;

            AssetDatabase.AddObjectToAsset(so, this);

            _installers.Add(so as IUnityInstaller);

            if (save)
            {
                AssetDatabase.SaveAssets();
            }
        }

        internal void Remove(int index, bool save = true)
        {
            var installer = _installers[index];

            var path = AssetDatabase.GetAssetPath(installer);

            if (string.IsNullOrEmpty(path) is false)
            {
                var assets = AssetDatabase.LoadAllAssetsAtPath(path);

                foreach (var asset in assets)
                {
                    if (AssetDatabase.IsSubAsset(asset) && asset == installer)
                    {
                        AssetDatabase.RemoveObjectFromAsset(asset);

                        _installers.Remove(installer);
                    }
                }
            }

            if (save)
            {
                AssetDatabase.SaveAssets();
            }
        }

        internal void Reset(int index, bool save = true)
        {
            var installer = _installers[index];

            installer.Reset();

            if (save)
            {
                AssetDatabase.SaveAssets();
            }
        }
    }
}
#endif
