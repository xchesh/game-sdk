using System.Collections.Generic;
using System.Linq;
using GameSdk.Core.Essentials;
using GameSdk.Core.Parameters;

namespace GameSdk.Core.Conditions
{
    public class ConditionsSystem : IConditionsSystem
    {
        private readonly InstancesManager<ICondition> _conditionsManager = new();

        InstancesManager<ICondition> IConditionsSystem.Manager => _conditionsManager;


        public ConditionsSystem(IEnumerable<ICondition> conditions)
        {
            foreach (var condition in conditions)
            {
                _conditionsManager.Register(condition.DataType, condition);

                if (condition is ICondition.IWithSystem withSystem)
                {
                    withSystem.SetSystem(this);
                }
            }
        }

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