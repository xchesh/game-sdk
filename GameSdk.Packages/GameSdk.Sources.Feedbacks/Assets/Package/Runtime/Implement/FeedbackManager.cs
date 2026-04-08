using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace GameSdk.Sources.Feedbacks
{
    public static class FeedbackManager
    {
        public const string TAG = "Feedbacks";

        private static readonly IDictionary<Type, IFeedbackStrategy> strategies = new Dictionary<Type, IFeedbackStrategy>();

        public static async Awaitable PlayFeedback<T>(T data, FeedbackPlaybackType playbackType = FeedbackPlaybackType.PARALLEL, CancellationToken cancellationToken = default, params object[] parameters) where T : IFeedbackData
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

        public static async Awaitable PlayFeedback(IEnumerable<IFeedbackData> feedbacks, FeedbackPlaybackType playbackType = FeedbackPlaybackType.PARALLEL, CancellationToken cancellationToken = default, params object[] parameters)
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

        public static void RegisterStrategy<T>(T strategy) where T : IFeedbackStrategy
        {
            strategies.TryAdd(strategy.DataType, strategy);
        }

        public static void UnregisterStrategy<T>(T strategy) where T : IFeedbackStrategy
        {
            strategies.Remove(strategy.DataType);
        }

        private static async Awaitable PlayFeedbackInParallel(IEnumerable<IFeedbackData> feedbacks, CancellationToken cancellationToken, params object[] parameters)
        {
            var tasks = new List<Awaitable>();

            foreach (var feedback in feedbacks)
            {
                tasks.Add(PlayFeedback(feedback, cancellationToken, parameters));
            }

            await tasks.WaitAll();
        }

        private static async Awaitable PlayFeedbackSequentially(IEnumerable<IFeedbackData> feedbacks, CancellationToken cancellationToken, params object[] parameters)
        {
            foreach (var feedback in feedbacks)
            {
                await PlayFeedback(feedback, cancellationToken, parameters);
            }
        }

        private static async Awaitable PlayFeedback<T>(T data, CancellationToken cancellationToken, params object[] parameters) where T : IFeedbackData
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
