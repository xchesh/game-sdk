using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace GameSdk.Sources.Feedbacks
{
    [Serializable]
    public class CameraShakeData : IFeedbackData
    {
        [SerializeField] private float _duration;
        [SerializeField] private float _intensity;

        public float Duration => _duration;
        public float Intensity => _intensity;

        [RequiredMember]
        public CameraShakeData()
        {
        }

        [RequiredMember]
        public CameraShakeData(float duration, float intensity)
        {
            _duration = duration;
            _intensity = intensity;
        }
    }
}
