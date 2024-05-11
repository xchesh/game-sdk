using System.Collections.Generic;
using System.Linq;
using GameSdk.Core.Essentials;
using GameSdk.Core.Parameters;

namespace GameSdk.Core.Rewards.Implement
{
    public class RewardsSystem : IRewardsSystem
    {
        private readonly InstancesManager<IReward> _rewardsManager = new();

        InstancesManager<IReward> IRewardsSystem.Manager => _rewardsManager;

        public RewardsSystem(IEnumerable<IReward> rewards)
        {
            foreach (var reward in rewards)
            {
                _rewardsManager.Register(reward.DataType, reward);

                if (reward is IReward.IWithSystem withSystem)
                {
                    withSystem.SetSystem(this);
                }
            }
        }

        public bool CanClaim(IRewardData rewardData, params IParameter[] parameters)
        {
            return rewardData != null && _rewardsManager.Get(rewardData.GetType()).CanClaim(rewardData, parameters);
        }

        public bool CanClaim(IEnumerable<IRewardData> rewardsData, params IParameter[] parameters)
        {
            return rewardsData != null && rewardsData.All(data => CanClaim(data, parameters));
        }

        public IRewardData Claim(IRewardData rewardData, params IParameter[] parameters)
        {
            return _rewardsManager.Get(rewardData.GetType()).Claim(rewardData, parameters);
        }

        public IEnumerable<IRewardData> Claim(IEnumerable<IRewardData> rewardsData, params IParameter[] parameters)
        {
            return rewardsData.Select(data => Claim(data, parameters));
        }

        public bool TryClaim(IRewardData rewardData, out IRewardData result, params IParameter[] parameters)
        {
            result = null;

            if (CanClaim(rewardData, parameters))
            {
                result = Claim(rewardData, parameters);

                return result != null;
            }

            return false;
        }

        public bool TryClaim(IEnumerable<IRewardData> rewardsData, out IEnumerable<IRewardData> result,
            params IParameter[] parameters)
        {
            result = null;

            var list = rewardsData as IRewardData[] ?? rewardsData.ToArray();

            if (CanClaim(list, parameters))
            {
                result = Claim(list, parameters);

                return result != null && result.Any();
            }

            return false;
        }
    }
}