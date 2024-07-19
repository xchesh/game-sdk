using System;
using Newtonsoft.Json;
using UnityEngine;

namespace GameSdk.Services.GraphicQuality
{
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public struct DeviceModelsConditionData : IGraphicQualityConditionData
    {
        [SerializeField, JsonProperty("deviceModels")] private string[] _deviceModels;

        public readonly string[] DeviceModels => _deviceModels;
        public readonly string Key => "device_models";
    }
}