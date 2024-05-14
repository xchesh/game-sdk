using System;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Rendering;

namespace GameSdk.Services.GraphicQuality
{
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public struct GraphicsDeviceTypeConditionData : IGraphicQualityConditionData
    {
        [SerializeField, JsonProperty("deviceType")] private GraphicsDeviceType _deviceType;

        public GraphicsDeviceType DeviceType => _deviceType;

        public string Key => "graphics_device_type";
    }
}