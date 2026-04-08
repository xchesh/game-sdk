using System;
using UnityEngine;

namespace GameSdk.Sources.Feedbacks
{
    public static class AwaitableExtensions
    {
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
    }
}
