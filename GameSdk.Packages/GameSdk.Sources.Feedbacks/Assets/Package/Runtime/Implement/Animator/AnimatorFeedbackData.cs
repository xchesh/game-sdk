using System;
using UnityEngine;

namespace GameSdk.Sources.Feedbacks
{
    [Serializable]
    public class AnimatorFeedbackData : IFeedbackData
    {
        [SerializeField]
        private UnityEngine.Animator _animator;

        [SerializeField, AnimatorTriggerDropdown("_animator")]
        private string _triggerName;

        [SerializeField]
        private float _duration = 1f;

        public UnityEngine.Animator Animator => _animator;
        public string TriggerName => _triggerName;
        public float Duration => _duration;

        public AnimatorFeedbackData()
        {
        }

        public AnimatorFeedbackData(UnityEngine.Animator animator, string triggerName, float duration)
        {
            _animator = animator;
            _triggerName = triggerName;
            _duration = duration;
        }
    }
}
