using GameSdk.Core.Conditions;
using GameSdk.Core.Parameters;

namespace GameSdk.Services.GraphicQuality
{
    public class DeviceIdCondition : ICondition<DeviceIdConditionData>
    {
        public bool Evaluate(DeviceIdConditionData data, params IParameter[] parameters)
        {
            var deviceId = UnityEngine.SystemInfo.deviceUniqueIdentifier;

            if (parameters.TryGetParameter<DeviceIdParameter>(out var deviceUniqueIdentifierParameter))
            {
                deviceId = deviceUniqueIdentifierParameter.DeviceId;
            }

            return data.DeviceId == deviceId || data.DeviceId.Contains(deviceId);
        }
    }
}