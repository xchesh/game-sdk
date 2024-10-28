using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace GameSdk.Sources.Feedbacks
{
    public static class FeedbackManager
    {
        public const string TAG = "Feedbacks";

        private static readonly IReadOnlyDictionary<System.Type, IFeedbackStrategy> strategies = FeedbackStrategyCache.Strategies;

        public static async UniTask PlayFeedback<T>(T data, FeedbackPlaybackType playbackType = FeedbackPlaybackType.PARALLEL, CancellationToken cancellationToken = default, params object[] parameters) where T : IFeedbackData
        {
            if (playbackType == FeedbackPlaybackType.PARALLEL)
            {
                await PlayFeedbackInParallel(new List<IFeedbackData> { data }, cancellationToken, parameters);
            }
            else
            {
                await PlayFeedbackSequentially(new List<IFeedbackData> { data }, cancellationToken, parameters);
            }
        }

        public static async UniTask PlayFeedback(IEnumerable<IFeedbackData> feedbacks, FeedbackPlaybackType playbackType = FeedbackPlaybackType.PARALLEL, CancellationToken cancellationToken = default, params object[] parameters)
        {
            if (playbackType == FeedbackPlaybackType.PARALLEL)
            {
                await PlayFeedbackInParallel(feedbacks, cancellationToken, parameters);
            }
            else
            {
                await PlayFeedbackSequentially(feedbacks, cancellationToken, parameters);
            }
        }

        private static async UniTask PlayFeedbackInParallel(IEnumerable<IFeedbackData> feedbacks, CancellationToken cancellationToken, params object[] parameters)
        {
            var tasks = new List<UniTask>();

            foreach (var feedback in feedbacks)
            {
                tasks.Add(PlayFeedback(feedback, cancellationToken, parameters));
            }

            await UniTask.WhenAll(tasks);

            tasks.Clear();
        }

        private static async UniTask PlayFeedbackSequentially(IEnumerable<IFeedbackData> feedbacks, CancellationToken cancellationToken, params object[] parameters)
        {
            foreach (var feedback in feedbacks)
            {
                await PlayFeedback(feedback, cancellationToken, parameters);
            }
        }

        private static async UniTask PlayFeedback<T>(T data, CancellationToken cancellationToken, params object[] parameters) where T : IFeedbackData
        {
            var dataType = data.GetType();

            if (strategies.TryGetValue(dataType, out var strategy))
            {
                if (strategy is IFeedbackStrategy<T> feedbackStrategy)
                {
                    await feedbackStrategy.Execute(data, cancellationToken, parameters);
                }
            }
        }
    }
}
