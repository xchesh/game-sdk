using System.Collections.Generic;
using GameSdk.Sources.Core.Common;
using GameSdk.Sources.Core.Essentials;

namespace GameSdk.Sources.Core.Conditions
{
    public interface IConditionsSystem
    {
        internal InstancesManager<ICondition> Manager { get; }

        bool Check(IConditionData conditionData, params IParameter[] parameters);
        bool CheckAny(IEnumerable<IConditionData> conditionsData, params IParameter[] parameters);
        bool CheckAll(IEnumerable<IConditionData> conditionsData, params IParameter[] parameters);
    }
}
