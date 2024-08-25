using System.Collections.Generic;

namespace GameSdk.Services.GraphicQuality
{
    public interface IGraphicQualityConfig {
        bool AutoEnable { get; }
        int DefaultFrameRate { get; }
        int DefaultQualityLevel { get; }
        List<GraphicQualityPreset> Presets { get; }

        IGraphicQualityPreset GetDefaultPreset();
    }
}
