using System.Threading;
using Cysharp.Threading.Tasks;

namespace GameSdk.Sources.Feedbacks
{
    public interface IFeedbackStrategy
    {
    }

    public interface IFeedbackStrategy<in T> : IFeedbackStrategy where T : IFeedbackData
    {
        UniTask Execute(T data, CancellationToken cancellationToken, params object[] parameters);
    }
}
