using Newtonsoft.Json;
using UnityEngine;

namespace GameSdk.Services.GraphicQuality
{
    // Code here
    [System.Serializable]
    public class GraphicQualityPreset : IGraphicQualityPreset
    {
        [HideInInspector, SerializeField]
        internal string _qualityName;

        [SerializeField, JsonProperty("platform")]
        private RuntimePlatform _platform = RuntimePlatform.Android;

        [SerializeField, JsonProperty("quality_level"), GraphicQuality]
        private int _qualityLevel = 0;

        [SerializeField, JsonProperty("max_frame_rate")]
        private int _maxFrameRate = 60;

        [SerializeReference, JsonProperty("condition"), Space]
        private IGraphicQualityConditionData _condition;

        public RuntimePlatform Platform => _platform;
        public int QualityLevel => _qualityLevel;
        public int MaxFrameRate => _maxFrameRate;
        public IGraphicQualityConditionData Condition => _condition;

        public GraphicQualityPreset(RuntimePlatform platform, int qualityLevel, int maxFrameRate, IGraphicQualityConditionData condition)
        {
            _platform = platform;
            _qualityLevel = qualityLevel;
            _maxFrameRate = maxFrameRate;
            _condition = condition;
        }
    }
}