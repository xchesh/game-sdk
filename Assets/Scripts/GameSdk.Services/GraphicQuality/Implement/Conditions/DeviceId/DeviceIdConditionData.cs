using Newtonsoft.Json;
using System;
using UnityEngine;

namespace GameSdk.Services.GraphicQuality
{
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public struct DeviceIdConditionData : IGraphicQualityConditionData
    {
        [SerializeField, JsonProperty("deviceId")] private string _deviceId;

        public readonly string DeviceId => _deviceId;
        public readonly string Key => "device_id";
    }
}
