using System.Linq;
using GameSdk.Core.Common;
using GameSdk.Core.Conditions;

namespace GameSdk.Services.GraphicQuality
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
