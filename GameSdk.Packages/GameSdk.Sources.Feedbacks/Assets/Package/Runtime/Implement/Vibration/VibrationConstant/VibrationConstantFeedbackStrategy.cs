using System;
using System.Threading;
using Lofelt.NiceVibrations;
using UnityEngine;

namespace GameSdk.Sources.Feedbacks
{
    public class VibrationConstantFeedbackStrategy : IFeedbackStrategy<VibrationConstantFeedbackData>
    {
        public async Awaitable Execute(VibrationConstantFeedbackData data, CancellationToken cancellationToken, params object[] parameters)
        {
            HapticPatterns.PlayConstant(data.Amplitude, data.Frequency, data.Duration);
            await Awaitable.WaitForSecondsAsync(data.Duration, cancellationToken).SuppressCancellationThrow();
        }
    }
}
