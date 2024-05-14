using GameSdk.Core.Conditions;
using Newtonsoft.Json;
using System;
using UnityEngine;

namespace GameSdk.Services.GraphicQuality
{
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public struct DeviceIdConditionData : IConditionData
    {
        [SerializeField, JsonProperty("deviceId")] private string _deviceId;

        public readonly string DeviceId => _deviceId;
        public readonly string Key => "device_id";
    }
}