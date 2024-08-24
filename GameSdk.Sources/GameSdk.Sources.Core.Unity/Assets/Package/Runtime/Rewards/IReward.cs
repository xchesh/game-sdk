using System;
using GameSdk.Sources.Core.Common;

namespace GameSdk.Sources.Core.Rewards
{
    public interface IReward
    {
        Type DataType { get; }

        bool CanClaim(IRewardData data, params IParameter[] parameters);
        IRewardResult Claim(IRewardData data, params IParameter[] parameters);
    }
}
