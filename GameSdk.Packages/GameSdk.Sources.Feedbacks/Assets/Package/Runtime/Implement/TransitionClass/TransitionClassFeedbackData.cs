using UnityEngine;

namespace GameSdk.Sources.Feedbacks
{
    [System.Serializable]
    public class TransitionClassFeedbackData : IFeedbackData
    {
        [SerializeField] private string _transitionClass;

        public string TransitionClass => _transitionClass;

        public TransitionClassFeedbackData()
        {
        }

        public TransitionClassFeedbackData(string transitionClass)
        {
            _transitionClass = transitionClass;
        }
    }
}
