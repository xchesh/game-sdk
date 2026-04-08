using System;
using System.Threading;
using UnityEngine;

namespace GameSdk.Sources.Feedbacks
{
    public static class AwaitableFeedbackUtility
    {
        public static async Awaitable WaitUntil(Func<bool> predicate, CancellationToken cancellationToken)
        {
            while (!predicate() && !cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await Awaitable.NextFrameAsync(cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }
        }
    }
}
