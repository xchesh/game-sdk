using System;
using GameSdk.Sources.Core.Common;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Rendering;

namespace GameSdk.Services.GraphicQuality
{
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public struct GraphicsDeviceTypeParameter : IParameter
    {
        [SerializeField, JsonProperty("deviceType")] private GraphicsDeviceType _deviceType;

        public readonly GraphicsDeviceType DeviceType => _deviceType;

        public GraphicsDeviceTypeParameter(GraphicsDeviceType deviceType)
        {
            _deviceType = deviceType;
        }
    }
}
