using System.Collections.Generic;
using GameSdk.Core.Essentials;
using GameSdk.Core.Parameters;

namespace GameSdk.Core.Rewards
{
    public interface IRewardsSystem
    {
        internal InstancesManager<IReward> Manager { get; }

        bool CanClaim(IRewardData rewardData, params IParameter[] parameters);
        bool CanClaim(IEnumerable<IRewardData> rewardsData, params IParameter[] parameters);

        IRewardData Claim(IRewardData rewardData, params IParameter[] parameters);
        IEnumerable<IRewardData> Claim(IEnumerable<IRewardData> rewardsData, params IParameter[] parameters);

        bool TryClaim(IRewardData rewardData, out IRewardData result, params IParameter[] parameters);

        bool TryClaim(IEnumerable<IRewardData> rewardsData, out IEnumerable<IRewardData> result,
            params IParameter[] parameters);
    }
}