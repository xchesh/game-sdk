using GameSdk.Sources.Core.Common;
using Newtonsoft.Json;
using System;
using System.Collections;
using UnityEngine;

namespace GameSdk.Sources.Services.GraphicQuality
{
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public struct DeviceIdParameter : IParameter
    {
        [SerializeField, JsonProperty("deviceId")] private string _deviceId;

        public readonly string DeviceId => _deviceId;
    }
}
