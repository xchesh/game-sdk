using GameSdk.Core.Conditions;
using UnityEngine;

namespace GameSdk.Services.GraphicQuality
{
    public interface IGraphicQualityPreset
    {
        RuntimePlatform Platform { get; }
        int QualityLevel { get; }
        int MaxFrameRate { get; }
        IConditionData Condition { get; }
    }
}
