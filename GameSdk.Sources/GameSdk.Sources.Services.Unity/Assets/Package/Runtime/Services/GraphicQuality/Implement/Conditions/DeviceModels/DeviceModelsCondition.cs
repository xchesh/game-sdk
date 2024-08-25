using System.Linq;
using GameSdk.Sources.Core.Common;
using GameSdk.Sources.Core.Conditions;

namespace GameSdk.Sources.Services.GraphicQuality
{
    public class DeviceModelsCondition : ICondition<DeviceModelsConditionData>
    {
        public bool Evaluate(DeviceModelsConditionData data, params IParameter[] parameters)
        {
            var deviceModel = UnityEngine.SystemInfo.deviceModel;

            if (parameters.TryGetParameter<DeviceModelsParameter>(out var deviceModelsParameter))
            {
                deviceModel = deviceModelsParameter.DeviceModel;
            }

            return data.DeviceModels.Contains(deviceModel);
        }
    }
}
