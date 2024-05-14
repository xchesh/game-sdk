using GameSdk.Core.Conditions;
using GameSdk.Core.Parameters;

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