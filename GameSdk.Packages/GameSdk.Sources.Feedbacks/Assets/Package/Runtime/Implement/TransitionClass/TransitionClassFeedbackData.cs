using UnityEngine;
using UnityEngine.Scripting;

namespace GameSdk.Sources.Feedbacks
{
    [System.Serializable]
    public class TransitionClassFeedbackData : IFeedbackData
    {
        [SerializeField] private string _transitionClass;

        public string TransitionClass => _transitionClass;

        [RequiredMember]
        public TransitionClassFeedbackData()
        {
        }

        [RequiredMember]
        public TransitionClassFeedbackData(string transitionClass)
        {
            _transitionClass = transitionClass;
        }
    }
}
