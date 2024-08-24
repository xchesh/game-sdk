using System;
using GameSdk.Sources.Core.Common;
using Newtonsoft.Json;
using UnityEngine;

namespace GameSdk.Services.GraphicQuality
{
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public struct DeviceModelsParameter : IParameter
    {
        [SerializeField, JsonProperty("deviceModels")] private string _deviceModel;

        public readonly string DeviceModel => _deviceModel;

        public DeviceModelsParameter(string deviceModel)
        {
            _deviceModel = deviceModel;
        }
    }
}
