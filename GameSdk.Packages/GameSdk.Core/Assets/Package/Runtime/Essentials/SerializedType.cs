using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace GameSdk.Core.Essentials
{
    [Serializable]
    public struct SerializedType
    {
        [SerializeField] private string _assemblyName;

        [SerializeField] private string _className;

        private Type _cachedType;

        public string AssemblyName => _assemblyName;

        public string ClassName => _className;

        public Type Value
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(_assemblyName) || string.IsNullOrEmpty(_className))
                    {
                        return null;
                    }

                    if (_cachedType == null)
                    {
                        var assembly = Assembly.Load(_assemblyName);

                        if (assembly != null)
                        {
                            _cachedType = assembly.GetType(_className);
                        }
                    }

                    return _cachedType;
                }
                catch (Exception ex)
                {
                    if (ex.GetType() != typeof(FileNotFoundException))
                    {
                        Debug.LogException(ex);
                    }

                    return null;
                }
            }
            set
            {
                if (value != null)
                {
                    _assemblyName = value.Assembly.FullName;
                    _className = value.FullName;
                }
                else
                {
                    _assemblyName = (_className = null);
                }
            }
        }

        public bool ValueChanged { get; set; }

        public override string ToString()
        {
            return (Value == null) ? "<none>" : Value.Name;
        }
    }
}
