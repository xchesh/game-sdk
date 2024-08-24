using System.Collections.Generic;
using GameSdk.Sources.Core.Common;

namespace GameSdk.Sources.Core.Rewards.RewardResults
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
