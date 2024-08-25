using GameSdk.Sources.Core.Common;
using GameSdk.Sources.Core.Conditions;

namespace GameSdk.Sources.Services.GraphicQuality
{
    public class MemorySizeCondition : ICondition<MemorySizeConditionData>
    {
        public bool Evaluate(MemorySizeConditionData data, params IParameter[] parameters)
        {
            var memorySize = UnityEngine.SystemInfo.systemMemorySize;

            if (parameters.TryGetParameter<MemorySizeParameter>(out var memorySizeParameter))
            {
                memorySize = memorySizeParameter.MemorySize;
            }

            return memorySize >= data.Min && memorySize <= data.Max;
        }
    }
}
