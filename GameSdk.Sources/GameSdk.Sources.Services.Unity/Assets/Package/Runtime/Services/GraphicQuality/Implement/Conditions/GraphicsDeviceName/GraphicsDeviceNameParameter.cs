using System;
using GameSdk.Sources.Core.Common;
using Newtonsoft.Json;
using UnityEngine;

namespace GameSdk.Sources.Services.GraphicQuality
{
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public struct GraphicsDeviceNameParameter : IParameter
    {
        [SerializeField, JsonProperty("deviceName")] private string _deviceName;

        public readonly string DeviceName => _deviceName;

        public GraphicsDeviceNameParameter(string deviceName)
        {
            _deviceName = deviceName;
        }
    }
}
