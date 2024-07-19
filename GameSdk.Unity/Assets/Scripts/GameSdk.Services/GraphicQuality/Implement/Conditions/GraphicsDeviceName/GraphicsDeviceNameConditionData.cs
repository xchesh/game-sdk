using System;
using Newtonsoft.Json;
using UnityEngine;

namespace GameSdk.Services.GraphicQuality
{
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public struct GraphicsDeviceNameConditionData : IGraphicQualityConditionData
    {
        [SerializeField, JsonProperty("deviceName")] private string _deviceName;

        public string Key => "graphics_device_name";
        public string DeviceName => _deviceName;
    }
}