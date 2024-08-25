using GameSdk.Sources.Core.Common;
using GameSdk.Sources.Core.Conditions;

namespace GameSdk.Sources.Services.GraphicQuality
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
