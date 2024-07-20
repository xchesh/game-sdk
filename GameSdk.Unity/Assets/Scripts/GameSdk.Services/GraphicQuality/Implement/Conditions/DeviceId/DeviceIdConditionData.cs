using Newtonsoft.Json;
using System;
using UnityEngine;
using GameSdk.Sources.Generated;

namespace GameSdk.Services.GraphicQuality
{
    [Serializable, JsonConvertable("device_id")]
    public partial struct DeviceIdConditionData : IGraphicQualityConditionData
    {
        [SerializeField, JsonProperty("deviceId")] private string _deviceId;

        public readonly string DeviceId => _deviceId;
    }
}
