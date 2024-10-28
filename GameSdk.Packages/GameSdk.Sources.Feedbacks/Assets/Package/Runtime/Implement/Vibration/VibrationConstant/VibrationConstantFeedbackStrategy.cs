using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Lofelt.NiceVibrations;

namespace GameSdk.Sources.Feedbacks
{
    public class VibrationConstantFeedbackStrategy : IFeedbackStrategy<VibrationConstantFeedbackData>
    {
        public async UniTask Execute(VibrationConstantFeedbackData data, CancellationToken cancellationToken, params object[] parameters)
        {
            HapticPatterns.PlayConstant(data.Amplitude, data.Frequency, data.Duration);
            await UniTask.Delay(TimeSpan.FromSeconds(data.Duration), cancellationToken: cancellationToken).SuppressCancellationThrow();
        }
    }
}
