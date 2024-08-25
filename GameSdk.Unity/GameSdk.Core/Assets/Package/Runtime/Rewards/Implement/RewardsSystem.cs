using System.Collections.Generic;
using System.Linq;
using GameSdk.Core.Common;
using GameSdk.Core.Essentials;

namespace GameSdk.Core.Rewards
{
    [JetBrains.Annotations.UsedImplicitly]
    public class RewardsSystem : IRewardsSystem
    {
        private readonly InstancesManager<IReward> _rewardsManager = new();

        InstancesManager<IReward> IRewardsSystem.Manager => _rewardsManager;

        public bool CanClaim(IRewardData rewardData, params IParameter[] parameters)
        {
            return rewardData != null && _rewardsManager.Get(rewardData.GetType()).CanClaim(rewardData, parameters);
        }

        public bool CanClaim(IEnumerable<IRewardData> rewardsData, params IParameter[] parameters)
        {
            return rewardsData != null && rewardsData.All(data => CanClaim(data, parameters));
        }

        public IRewardResult Claim(IRewardData rewardData, params IParameter[] parameters)
        {
            return _rewardsManager.Get(rewardData.GetType()).Claim(rewardData, parameters);
        }

        public IEnumerable<IRewardResult> Claim(IEnumerable<IRewardData> rewardsData, params IParameter[] parameters)
        {
            return rewardsData.Select(data => Claim(data, parameters));
        }
    }
}
