using GameSdk.Core.Common;
using Newtonsoft.Json;
using System;
using System.Collections;
using UnityEngine;

namespace GameSdk.Services.GraphicQuality
{
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public struct DeviceIdParameter : IParameter
    {
        [SerializeField, JsonProperty("deviceId")] private string _deviceId;

        public readonly string DeviceId => _deviceId;
    }
}
