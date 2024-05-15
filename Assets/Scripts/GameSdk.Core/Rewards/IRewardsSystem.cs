using System.Collections.Generic;
using GameSdk.Core.Common;
using GameSdk.Core.Essentials;

namespace GameSdk.Core.Rewards
{
    public interface IRewardsSystem
    {
        internal InstancesManager<IReward> Manager { get; }

        bool CanClaim(IRewardData rewardData, params IParameter[] parameters);
        bool CanClaim(IEnumerable<IRewardData> rewardsData, params IParameter[] parameters);

        IRewardResult Claim(IRewardData rewardData, params IParameter[] parameters);
        IEnumerable<IRewardResult> Claim(IEnumerable<IRewardData> rewardsData, params IParameter[] parameters);
    }
}