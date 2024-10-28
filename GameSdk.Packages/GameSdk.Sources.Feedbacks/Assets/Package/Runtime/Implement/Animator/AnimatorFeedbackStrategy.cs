using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace GameSdk.Sources.Feedbacks
{
    public class AnimatorFeedbackStrategy : IFeedbackStrategy<AnimatorFeedbackData>
    {
        public async UniTask Execute(AnimatorFeedbackData data, CancellationToken cancellationToken, params object[] parameters)
        {
            data.Animator.SetTrigger(data.TriggerName);

            await UniTask.Delay(TimeSpan.FromSeconds(data.Duration), cancellationToken: cancellationToken).SuppressCancellationThrow();
        }
    }
}
