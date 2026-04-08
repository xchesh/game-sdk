using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace GameSdk.Sources.Feedbacks
{
    public static class AwaitableFeedbackUtility
    {
        public static Awaitable Completed()
        {
            var completionSource = new AwaitableCompletionSource();
            completionSource.SetResult();
            return completionSource.Awaitable;
        }

        public static Awaitable WhenAll(IReadOnlyList<Awaitable> awaitables)
        {
            if (awaitables.Count == 0)
            {
                return Completed();
            }

            var completionSource = new AwaitableCompletionSource();
            var remaining = awaitables.Count;
            var wasCanceled = false;
            Exception exception = null;
            var gate = new object();

            foreach (var awaitable in awaitables)
            {
                _ = Observe(awaitable);
            }

            return completionSource.Awaitable;

            async Awaitable Observe(Awaitable awaitable)
            {
                try
                {
                    await awaitable;
                }
                catch (OperationCanceledException)
                {
                    lock (gate)
                    {
                        wasCanceled = true;
                    }
                }
                catch (Exception ex)
                {
                    lock (gate)
                    {
                        exception ??= ex;
                    }
                }

                TryComplete();
            }

            void TryComplete()
            {
                if (Interlocked.Decrement(ref remaining) != 0)
                {
                    return;
                }

                lock (gate)
                {
                    if (exception != null)
                    {
                        completionSource.TrySetException(exception);
                    }
                    else if (wasCanceled)
                    {
                        completionSource.TrySetCanceled();
                    }
                    else
                    {
                        completionSource.TrySetResult();
                    }
                }
            }
        }

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
