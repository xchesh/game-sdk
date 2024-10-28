using System;
using UnityEngine;

namespace GameSdk.Sources.Feedbacks
{
    [Serializable]
    public class CameraShakeData : IFeedbackData
    {
        [SerializeField] private float _duration;
        [SerializeField] private float _intensity;

        public float Duration => _duration;
        public float Intensity => _intensity;

        public CameraShakeData()
        {
        }

        public CameraShakeData(float duration, float intensity)
        {
            _duration = duration;
            _intensity = intensity;
        }
    }
}
