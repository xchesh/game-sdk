using System;
using GameSdk.Core.Parameters;

namespace GameSdk.Core.Conditions
{
    public interface ICondition
    {
        Type DataType { get; }

        bool Evaluate(IConditionData data, params IParameter[] parameters);
    }
}