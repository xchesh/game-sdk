using System.Collections.Generic;
using System.Linq;
using GameSdk.Core.Common;
using GameSdk.Core.Essentials;

namespace GameSdk.Core.Conditions
{
    [JetBrains.Annotations.UsedImplicitly]
    public class ConditionsSystem : IConditionsSystem
    {
        private readonly InstancesManager<ICondition> _conditionsManager = new();

        InstancesManager<ICondition> IConditionsSystem.Manager => _conditionsManager;

        public bool Check(IConditionData conditionData, params IParameter[] parameters)
        {
            return _conditionsManager.Get(conditionData.GetType()).Evaluate(conditionData, parameters);
        }

        public bool CheckAny(IEnumerable<IConditionData> conditionsData, params IParameter[] parameters)
        {
            return conditionsData.Any(cd => Check(cd, parameters));
        }

        public bool CheckAll(IEnumerable<IConditionData> conditionsData, params IParameter[] parameters)
        {
            return conditionsData.All(cd => Check(cd, parameters));
        }
    }
}
