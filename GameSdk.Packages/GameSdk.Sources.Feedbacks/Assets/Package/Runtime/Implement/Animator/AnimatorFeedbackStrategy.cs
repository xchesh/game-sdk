using System;
using System.Threading;
using UnityEngine;

namespace GameSdk.Sources.Feedbacks
{
    public class AnimatorFeedbackStrategy : IFeedbackStrategy<AnimatorFeedbackData>
    {
        public async Awaitable Execute(AnimatorFeedbackData data, CancellationToken cancellationToken, params object[] parameters)
        {
            data.Animator.SetTrigger(data.TriggerName);

            await Awaitable.WaitForSecondsAsync(data.Duration, cancellationToken).SuppressCancellationThrow();
        }
    }
}
