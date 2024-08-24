using GameSdk.Sources.Core.Common;

namespace GameSdk.Sources.Core.Rewards
{
    public interface IRewardResult
    {
        IRewardData RewardData { get; }
        string Placement { get; }
        IParameter[] Parameters { get; }
    }
}
