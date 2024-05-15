using GameSdk.Core.Common;
using GameSdk.Core.Conditions;

namespace GameSdk.Services.GraphicQuality
{
    public class GraphicsDeviceTypeCondition : ICondition<GraphicsDeviceTypeConditionData>
    {
        public bool Evaluate(GraphicsDeviceTypeConditionData data, params IParameter[] parameters)
        {
            var graphicsDeviceType = UnityEngine.SystemInfo.graphicsDeviceType;

            if (parameters.TryGetParameter<GraphicsDeviceTypeParameter>(out var graphicsDeviceTypeParameter))
            {
                graphicsDeviceType = graphicsDeviceTypeParameter.DeviceType;
            }

            return graphicsDeviceType == data.DeviceType;
        }
    }
}