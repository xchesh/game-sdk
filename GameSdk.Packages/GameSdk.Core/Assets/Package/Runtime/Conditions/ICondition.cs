using System;
using GameSdk.Core.Common;

namespace GameSdk.Core.Conditions
{
    public interface ICondition
    {
        Type DataType { get; }

        bool Evaluate(IConditionData data, params IParameter[] parameters);
    }

    public interface ICondition<T> : ICondition where T : IConditionData
    {
        Type ICondition.DataType => typeof(T);

        bool ICondition.Evaluate(IConditionData data, params IParameter[] parameters)
        {
            if (data is not T tData)
            {
                throw new ArgumentException($"Data type {data.GetType()} is not {typeof(T)}");
            }

            return Evaluate(tData, parameters);
        }

        bool Evaluate(T data, params IParameter[] parameters);
    }
}
