namespace GameSdk.Sources.Services.GraphicQuality
{
    public interface IGraphicQualityService
    {
        const string TAG = "Graphic Quality";

        IGraphicQualityPreset CurrentPreset { get; }

        void Initialize();

        void SetQualityPreset(IGraphicQualityPreset preset);
        void SetQualityLevel(int level);
        void SetFrameRate(int frameRate);
    }
}
