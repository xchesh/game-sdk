using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace GameSdk.Sources.Feedbacks
{
    public class FeedbackService : IFeedbackService
    {
        public async UniTask PlayFeedback<T>(T data, FeedbackPlaybackType playbackType = FeedbackPlaybackType.PARALLEL, CancellationToken cancellationToken = default, params object[] parameters) where T : IFeedbackData
        {
            await FeedbackManager.PlayFeedback(data, playbackType, cancellationToken, parameters);
        }

        public async UniTask PlayFeedback(IEnumerable<IFeedbackData> feedbacks, FeedbackPlaybackType playbackType = FeedbackPlaybackType.PARALLEL, CancellationToken cancellationToken = default, params object[] parameters)
        {
            await FeedbackManager.PlayFeedback(feedbacks, playbackType, cancellationToken, parameters);
        }
    }
}
