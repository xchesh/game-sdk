using System;
using Newtonsoft.Json;
using UnityEngine;
using GameSdk.Sources.Json.Generated;

namespace GameSdk.Sources.Services.GraphicQuality
{
    [Serializable, JsonConvertable("device_models")]
    public partial struct DeviceModelsConditionData : IGraphicQualityConditionData
    {
        [SerializeField, JsonProperty("deviceModels")] private string[] _deviceModels;

        public readonly string[] DeviceModels => _deviceModels;
    }
}
