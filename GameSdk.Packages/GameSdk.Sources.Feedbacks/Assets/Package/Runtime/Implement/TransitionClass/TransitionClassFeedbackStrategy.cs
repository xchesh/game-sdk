using System;
using System.Linq;
using System.Threading;
using GameSdk.Core.Loggers;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameSdk.Sources.Feedbacks
{
    public class TransitionClassFeedbackStrategy : IFeedbackStrategy<TransitionClassFeedbackData>
    {
        public async Awaitable Execute(TransitionClassFeedbackData data, CancellationToken cancellationToken, params object[] parameters)
        {
            if (parameters.Length == 0 || parameters[0] is not VisualElement visualElement)
            {
                SystemLog.LogWarning(FeedbackManager.TAG, "VisualElement must be provided as the first parameter.");

                return;
            }

            var completionSource = new AwaitableCompletionSource();

            visualElement.RemoveFromClassList(data.TransitionClass);
            visualElement.RegisterCallback<TransitionEndEvent>(OnTransitionEndEvent);
            visualElement.schedule.Execute(() => { visualElement.AddToClassList(data.TransitionClass); }).ExecuteLater(0);
            using var cancellationRegistration = cancellationToken.Register(() => completionSource.TrySetCanceled());

            try
            {
                await completionSource.Awaitable;
            }
            catch (OperationCanceledException)
            {
                visualElement.RemoveFromClassList(data.TransitionClass);
            }
            finally
            {
                visualElement.UnregisterCallback<TransitionEndEvent>(OnTransitionEndEvent);
            }

            return;

            void OnTransitionEndEvent(TransitionEndEvent evt)
            {
                if (evt.stylePropertyNames.Contains(data.TransitionClass) is false)
                {
                    return;
                }

                completionSource.TrySetResult();
            }
        }
    }
}
