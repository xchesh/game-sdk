using System.Collections.Generic;

namespace GameSdk.Core.Conditions
{
    public class ConditionsContext : IConditionsContext
    {
        private readonly IEnumerable<ICondition> _conditions;
        private readonly IConditionsSystem _conditionsSystem;

        public ConditionsContext(IEnumerable<ICondition> conditions, IConditionsSystem conditionsSystem)
        {
            _conditions = conditions;
            _conditionsSystem = conditionsSystem;

            foreach (var condition in _conditions)
            {
                _conditionsSystem.Manager.Register(condition.DataType, condition);

                if (condition is ICondition.IWithSystem withSystem)
                {
                    withSystem.SetSystem(_conditionsSystem);
                }
            }
        }

        public void Dispose()
        {
            foreach (var condition in _conditions)
            {
                _conditionsSystem.Manager.Unregister(condition.DataType);
            }
        }
    }
}
