using System;
using Lofelt.NiceVibrations;
using UnityEngine;

namespace GameSdk.Sources.Feedbacks
{
    [Serializable]
    public class VibrationPresetFeedbackData : IFeedbackData
    {
        [SerializeField] private HapticPatterns.PresetType _vibrationType;

        public HapticPatterns.PresetType VibrationType => _vibrationType;

        public VibrationPresetFeedbackData()
        {
        }

        public VibrationPresetFeedbackData(HapticPatterns.PresetType vibrationType)
        {
            _vibrationType = vibrationType;
        }
    }
}
