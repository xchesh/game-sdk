using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Lofelt.NiceVibrations;

namespace GameSdk.Sources.Feedbacks
{
    public class VibrationPresetFeedbackStrategy : IFeedbackStrategy<VibrationPresetFeedbackData>
    {
        public async UniTask Execute(VibrationPresetFeedbackData data, CancellationToken cancellationToken, params object[] parameters)
        {
            HapticPatterns.PlayPreset(data.VibrationType);
            var duration = HapticPatterns.GetPresetDuration(data.VibrationType);
            await UniTask.Delay(TimeSpan.FromSeconds(duration), cancellationToken: cancellationToken).SuppressCancellationThrow();
        }
    }
}
