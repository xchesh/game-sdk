using System;
using GameSdk.Core.Conditions;
using Newtonsoft.Json;
using UnityEngine;

namespace GameSdk.Services.GraphicQuality
{
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public struct MemorySizeConditionData : IConditionData
    {
        [SerializeField, JsonProperty("memoryMin")] private int _min;
        [SerializeField, JsonProperty("memoryMax")] private int _max;

        public readonly int Min => _min;
        public readonly int Max => _max;
        public readonly string Key => "memory_size";
    }
}