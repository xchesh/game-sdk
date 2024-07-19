using System;
using GameSdk.Core.Conditions;
using GameSdk.Core.Loggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameSdk.Services.GraphicQuality
{
    public class GraphicQualityService : IGraphicQualityService
    {
        private readonly ISystemLogger _systemLogger;
        private readonly IGraphicQualityConfig _config;
        private readonly IConditionsSystem _conditionsSystem;

        public IGraphicQualityPreset CurrentPreset { get; private set; }

        public GraphicQualityService(ISystemLogger systemLogger, IGraphicQualityConfig config, IConditionsSystem conditionsSystem)
        {
            Assert.IsNotNull(config, "Graphic quality config is not set");

            _systemLogger = systemLogger;
            _config = config;
            _conditionsSystem = conditionsSystem;
        }

        public void Initialize()
        {
            if (_config.AutoEnable is false)
            {
                _systemLogger.Log(LogType.Log, IGraphicQualityService.TAG, "Graphic quality service is disabled");

                return;
            }

            var preset = GetPresetByConditions();

            _systemLogger.Log(LogType.Log, IGraphicQualityService.TAG, $"Graphic quality level: {preset.QualityLevel}; Frame rate: {preset.MaxFrameRate}");

            SetQualityPreset(preset);
        }

        public void SetFrameRate(int frameRate)
        {
            var targetFrameRate = Mathf.Min((int)Screen.currentResolution.refreshRateRatio.value, frameRate);

            Application.targetFrameRate = targetFrameRate;
        }

        public void SetQualityLevel(int level)
        {
            if (level < 0 || level >= QualitySettings.names.Length)
            {
                _systemLogger.LogException(new ArgumentOutOfRangeException(nameof(level), level, "Quality level is out of range"));

                return;
            }

            QualitySettings.SetQualityLevel(level);
        }

        public void SetQualityPreset(IGraphicQualityPreset preset)
        {
            CurrentPreset = preset;

            SetQualityLevel(preset.QualityLevel);
            SetFrameRate(preset.MaxFrameRate);
        }

        private IGraphicQualityPreset GetPresetByConditions()
        {
            foreach (var preset in _config.Presets)
            {
                if (preset.Platform == Application.platform && _conditionsSystem.Check(preset.Condition))
                {
                    return preset;
                }
            }

            return _config.GetDefaultPreset();
        }
    }
}