using UnityEngine;
using System.Collections.Generic;

namespace GameSdk.Services.GraphicQuality
{
    [CreateAssetMenu(fileName = "GameSDK_GraphicQualityConfig", menuName = "GameSds/Graphic Quality Config")]
    public class GraphicQualityConfig : ScriptableObject, IGraphicQualityConfig
    {
        [SerializeField] private bool _autoEnable = true;
        [SerializeField, Space] private int _defaultFrameRate = 60;
        [SerializeField, GraphicQuality] private int _defaultQualityLevel;
        [SerializeField, Space] private List<GraphicQualityPreset> presets = new();

        public bool AutoEnable => _autoEnable;
        public int DefaultFrameRate => _defaultFrameRate;
        public int DefaultQualityLevel => _defaultQualityLevel;
        public List<GraphicQualityPreset> Presets => presets;

        public IGraphicQualityPreset GetDefaultPreset()
        {
            return new GraphicQualityPreset(Application.platform, DefaultQualityLevel, DefaultQualityLevel, null);
        }
    }
}