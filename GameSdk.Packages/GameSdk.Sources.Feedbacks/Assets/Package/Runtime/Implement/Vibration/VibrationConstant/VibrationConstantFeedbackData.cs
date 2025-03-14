﻿using UnityEngine;
using UnityEngine.Scripting;

namespace GameSdk.Sources.Feedbacks
{
    [System.Serializable]
    public class VibrationConstantFeedbackData : IFeedbackData
    {
        [SerializeField] private float _amplitude;
        [SerializeField] private float _frequency;
        [SerializeField] private float _duration;

        public float Amplitude => _amplitude;
        public float Frequency => _frequency;
        public float Duration => _duration;

        [RequiredMember]
        public VibrationConstantFeedbackData()
        {
        }

        [RequiredMember]
        public VibrationConstantFeedbackData(float amplitude, float frequency, float duration)
        {
            _amplitude = amplitude;
            _frequency = frequency;
            _duration = duration;
        }
    }
}
