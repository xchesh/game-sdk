using System;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Rendering;
using GameSdk.Sources.Json;

namespace GameSdk.Services.GraphicQuality
{
    [Serializable, JsonConvertable("graphics_device_type")]
    public partial struct GraphicsDeviceTypeConditionData : IGraphicQualityConditionData
    {
        [SerializeField, JsonProperty("deviceType")] private GraphicsDeviceType _deviceType;

        public GraphicsDeviceType DeviceType => _deviceType;
    }
}
