using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace GameSdk.Sources.Feedbacks
{
    public interface IFeedbackService
    {
        UniTask PlayFeedback<T>(T data, FeedbackPlaybackType playbackType = FeedbackPlaybackType.PARALLEL, CancellationToken cancellationToken = default, params object[] parameters) where T : IFeedbackData;
        UniTask PlayFeedback(IEnumerable<IFeedbackData> feedbacks, FeedbackPlaybackType playbackType = FeedbackPlaybackType.PARALLEL, CancellationToken cancellationToken = default, params object[] parameters);
    }
}
