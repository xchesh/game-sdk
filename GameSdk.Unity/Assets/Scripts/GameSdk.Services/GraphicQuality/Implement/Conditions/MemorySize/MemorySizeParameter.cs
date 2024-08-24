using System;
using GameSdk.Sources.Core.Common;
using Newtonsoft.Json;
using UnityEngine;

namespace GameSdk.Services.GraphicQuality
{
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public struct MemorySizeParameter : IParameter
    {
        [SerializeField, JsonProperty("memorySize")] private int _memorySize;

        public readonly int MemorySize => _memorySize;

        public MemorySizeParameter(int memorySize)
        {
            _memorySize = memorySize;
        }
    }
}
