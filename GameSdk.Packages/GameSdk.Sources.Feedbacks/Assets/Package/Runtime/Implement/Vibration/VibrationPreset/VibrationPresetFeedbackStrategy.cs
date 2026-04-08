using System;
using System.Threading;
using Lofelt.NiceVibrations;
using UnityEngine;

namespace GameSdk.Sources.Feedbacks
{
    public class VibrationPresetFeedbackStrategy : IFeedbackStrategy<VibrationPresetFeedbackData>
    {
        public async Awaitable Execute(VibrationPresetFeedbackData data, CancellationToken cancellationToken, params object[] parameters)
        {
            HapticPatterns.PlayPreset(data.VibrationType);
            var duration = HapticPatterns.GetPresetDuration(data.VibrationType);
            await Awaitable.WaitForSecondsAsync(duration, cancellationToken).SuppressCancellationThrow();
        }
    }
}
