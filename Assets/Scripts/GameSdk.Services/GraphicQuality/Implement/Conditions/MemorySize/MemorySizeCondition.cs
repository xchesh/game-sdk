using GameSdk.Core.Conditions;
using GameSdk.Core.Parameters;

namespace GameSdk.Services.GraphicQuality
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