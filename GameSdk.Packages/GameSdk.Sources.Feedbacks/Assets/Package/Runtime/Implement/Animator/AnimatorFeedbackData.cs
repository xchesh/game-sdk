using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace GameSdk.Sources.Feedbacks
{
    [Serializable]
    public class AnimatorFeedbackData : IFeedbackData
    {
        [SerializeField]
        private Animator _animator;

        [SerializeField, AnimatorTriggerDropdown("_animator")]
        private string _triggerName;

        [SerializeField]
        private float _duration = 1f;

        public Animator Animator => _animator;
        public string TriggerName => _triggerName;
        public float Duration => _duration;

        [RequiredMember]
        public AnimatorFeedbackData()
        {
        }

        [RequiredMember]
        public AnimatorFeedbackData(Animator animator, string triggerName, float duration)
        {
            _animator = animator;
            _triggerName = triggerName;
            _duration = duration;
        }
    }
}
