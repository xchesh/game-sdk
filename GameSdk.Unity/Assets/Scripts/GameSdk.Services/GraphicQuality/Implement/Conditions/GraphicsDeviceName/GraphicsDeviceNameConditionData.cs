using System;
using Newtonsoft.Json;
using UnityEngine;
using GameSdk.Sources.Generated;

namespace GameSdk.Services.GraphicQuality
{
    [Serializable, JsonConvertable("graphics_device_name")]
    public partial struct GraphicsDeviceNameConditionData : IGraphicQualityConditionData
    {
        [SerializeField, JsonProperty("deviceName")] private string _deviceName;

        public string DeviceName => _deviceName;
    }
}
