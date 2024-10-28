using System.Collections.Generic;
using UnityEngine.Scripting;

namespace GameSdk.Sources.Feedbacks
{
    public class FeedbackContext : IFeedbackContext
    {
        private readonly IEnumerable<IFeedbackStrategy> _feedbackStrategies;

        [RequiredMember]
        public FeedbackContext(IEnumerable<IFeedbackStrategy> feedbackStrategies)
        {
            _feedbackStrategies = feedbackStrategies;

            foreach (var feedbackStrategy in _feedbackStrategies)
            {
                FeedbackManager.RegisterStrategy(feedbackStrategy);
            }
        }

        public void Dispose()
        {
            foreach (var feedbackStrategy in _feedbackStrategies)
            {
                FeedbackManager.UnregisterStrategy(feedbackStrategy);
            }
        }
    }
}
