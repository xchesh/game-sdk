using System.Threading;
using UnityEngine;

namespace GameSdk.Sources.Feedbacks
{
    public interface IFeedbackStrategy
    {
        System.Type DataType { get; }
    }

    public interface IFeedbackStrategy<in T> : IFeedbackStrategy where T : IFeedbackData
    {
        System.Type IFeedbackStrategy.DataType => typeof(T);

        Awaitable Execute(T data, CancellationToken cancellationToken, params object[] parameters);
    }
}
