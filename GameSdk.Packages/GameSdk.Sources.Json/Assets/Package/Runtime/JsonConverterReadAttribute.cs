using System;

namespace GameSdk.Sources.Json
{
    [AttributeUsage(AttributeTargets.Interface, Inherited = false, AllowMultiple = false)]
    public sealed class JsonConverterReadAttribute : Attribute
    {
    }
}
