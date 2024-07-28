using System;
using Newtonsoft.Json;
using UnityEngine;
using GameSdk.Sources.Json.Generated;

namespace GameSdk.Services.GraphicQuality
{
    [Serializable, JsonConvertable("memory_size")]
    public partial struct MemorySizeConditionData : IGraphicQualityConditionData
    {
        [SerializeField, JsonProperty("memoryMin")] private int _min;
        [SerializeField, JsonProperty("memoryMax")] private int _max;

        public readonly int Min => _min;
        public readonly int Max => _max;
    }
}
