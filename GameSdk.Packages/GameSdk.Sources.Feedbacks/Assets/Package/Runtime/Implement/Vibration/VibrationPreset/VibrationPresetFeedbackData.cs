using System;
using Lofelt.NiceVibrations;
using UnityEngine;
using UnityEngine.Scripting;

namespace GameSdk.Sources.Feedbacks
{
    [Serializable]
    public class VibrationPresetFeedbackData : IFeedbackData
    {
        [SerializeField] private HapticPatterns.PresetType _vibrationType;

        public HapticPatterns.PresetType VibrationType => _vibrationType;

        [RequiredMember]
        public VibrationPresetFeedbackData()
        {
        }

        [RequiredMember]
        public VibrationPresetFeedbackData(HapticPatterns.PresetType vibrationType)
        {
            _vibrationType = vibrationType;
        }
    }
}
