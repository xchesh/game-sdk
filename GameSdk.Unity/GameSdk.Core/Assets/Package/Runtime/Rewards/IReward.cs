using System;
using GameSdk.Core.Common;

namespace GameSdk.Core.Rewards
{
    public interface IReward
    {
        Type DataType { get; }

        bool CanClaim(IRewardData data, params IParameter[] parameters);
        IRewardResult Claim(IRewardData data, params IParameter[] parameters);
    }
}
