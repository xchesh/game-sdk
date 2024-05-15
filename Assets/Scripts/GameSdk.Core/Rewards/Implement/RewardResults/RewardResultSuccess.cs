using System.Collections.Generic;
using GameSdk.Core.Common;

namespace GameSdk.Core.Rewards.RewardResults
{
    public struct RewardResultSuccess : IRewardResult
    {
        public IRewardData RewardData { get; }
        public string Placement { get; }
        public IParameter[] Parameters { get; }

        public IEnumerable<IRewardData> ReceivedRewards { get; }

        public RewardResultSuccess(
            IRewardData rewardData,
            string placement,
            IEnumerable<IRewardData> receivedRewards,
            params IParameter[] parameters)
        {
            RewardData = rewardData;
            Placement = placement;
            ReceivedRewards = receivedRewards;
            Parameters = parameters;
        }
    }
}