using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSdk.Sources.Feedbacks
{
    public static class AwaitableExtensions
    {
        public static async Awaitable WaitAll(this IEnumerable<Awaitable> awaitables)
        {
            if (awaitables == null)
            {
                throw new ArgumentNullException(nameof(awaitables));
            }

            await Task.WhenAll(awaitables.Select(awaitable => awaitable.AsTask()));
        }

        public static Task AsTask(this Awaitable awaitable)
        {
            return AsTaskInternal(awaitable);
        }

        public static async Awaitable SuppressCancellationThrow(this Awaitable awaitable)
        {
            try
            {
                await awaitable;
            }
            catch (OperationCanceledException)
            {
            }
        }

        private static async Task AsTaskInternal(Awaitable awaitable)
        {
            await awaitable;
        }
    }
}
