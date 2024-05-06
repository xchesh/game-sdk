using System.Collections.Generic;

namespace GameSdk.Core.Rewards.Implement
{
    public class RewardsContext : IRewardsContext
    {
        private readonly IEnumerable<IReward> _rewards;
        private readonly IRewardsSystem _rewardsSystem;

        public RewardsContext(IEnumerable<IReward> rewards, IRewardsSystem rewardsSystem)
        {
            _rewards = rewards;
            _rewardsSystem = rewardsSystem;

            foreach (var reward in _rewards)
            {
                _rewardsSystem.Manager.Register(reward.DataType, reward);

                if (reward is IReward.IWithSystem withSystem)
                {
                    withSystem.SetSystem(_rewardsSystem);
                }
            }
        }

        public void Dispose()
        {
            foreach (var reward in _rewards)
            {
                _rewardsSystem.Manager.Unregister(reward.DataType);
            }
        }
    }
}
