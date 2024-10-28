using System.Threading;
using Cysharp.Threading.Tasks;

namespace GameSdk.Sources.Feedbacks
{
    public interface IFeedbackStrategy
    {
        System.Type DataType { get; }
    }

    public interface IFeedbackStrategy<in T> : IFeedbackStrategy where T : IFeedbackData
    {
        System.Type IFeedbackStrategy.DataType => typeof(T);

        UniTask Execute(T data, CancellationToken cancellationToken, params object[] parameters);
    }
}
