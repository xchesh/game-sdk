using System;
using GameSdk.Core.Parameters;

namespace GameSdk.Core.Rewards
{
    public interface IReward
    {
        Type DataType { get; }

        bool CanClaim(IRewardData data, params IParameter[] parameters);
        IRewardData Claim(IRewardData data, params IParameter[] parameters);

        public interface IWithSystem
        {
            IRewardsSystem System { get; }

            void SetSystem(IRewardsSystem system);
        }
    }
}
