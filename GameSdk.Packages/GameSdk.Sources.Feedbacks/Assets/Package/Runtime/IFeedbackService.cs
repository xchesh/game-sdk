using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace GameSdk.Sources.Feedbacks
{
    public interface IFeedbackService
    {
        Awaitable PlayFeedback<T>(T data, FeedbackPlaybackType playbackType = FeedbackPlaybackType.PARALLEL, CancellationToken cancellationToken = default, params object[] parameters) where T : IFeedbackData;
        Awaitable PlayFeedback(IEnumerable<IFeedbackData> feedbacks, FeedbackPlaybackType playbackType = FeedbackPlaybackType.PARALLEL, CancellationToken cancellationToken = default, params object[] parameters);
    }
}
