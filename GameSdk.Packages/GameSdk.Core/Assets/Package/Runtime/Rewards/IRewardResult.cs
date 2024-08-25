using GameSdk.Core.Common;

namespace GameSdk.Core.Rewards
{
    public interface IRewardResult
    {
        IRewardData RewardData { get; }
        string Placement { get; }
        IParameter[] Parameters { get; }
    }
}
