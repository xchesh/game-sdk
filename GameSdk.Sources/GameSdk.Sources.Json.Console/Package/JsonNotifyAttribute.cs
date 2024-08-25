using System;

namespace GameSdk.Sources.Json
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
    public sealed class JsonNotifyAttribute : Attribute
    {
        public JsonNotifyAttribute()
        {

        }
    }
}
