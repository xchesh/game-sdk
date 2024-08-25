using System;

namespace GameSdk.Sources.Json
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
    public sealed class JsonConvertableAttribute : Attribute
    {
        public string Key { get; }

        public JsonConvertableAttribute(string key)
        {

            Key = key;
        }
    }
}
