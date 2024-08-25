using GameSdk.Sources.Core.Common;
using GameSdk.Sources.Core.Conditions;

namespace GameSdk.Sources.Services.GraphicQuality
{
    public class GraphicsDeviceNameCondition : ICondition<GraphicsDeviceNameConditionData>
    {
        public bool Evaluate(GraphicsDeviceNameConditionData data, params IParameter[] parameters)
        {
            var graphicsDeviceName = UnityEngine.SystemInfo.graphicsDeviceName;

            if (parameters.TryGetParameter<GraphicsDeviceNameParameter>(out var graphicsDeviceNameParameter))
            {
                graphicsDeviceName = graphicsDeviceNameParameter.DeviceName;
            }

            return graphicsDeviceName == data.DeviceName || graphicsDeviceName.Contains(data.DeviceName);
        }
    }
}
