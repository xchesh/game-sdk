using System.Collections.Generic;
using GameSdk.Sources.Core.Common;
using GameSdk.Sources.Core.Essentials;

namespace GameSdk.Sources.Core.Rewards
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
