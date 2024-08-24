using GameSdk.Sources.Core.Common;

namespace GameSdk.Sources.Core.Rewards.RewardResults
{
    public struct RewardResultError : IRewardResult
    {
        public IRewardData RewardData { get; }
        public string Placement { get; }
        public IParameter[] Parameters { get; }

        public uint ErrorCode { get; }
        public string ErrorMessage { get; }

        public RewardResultError(
            IRewardData rewardData,
            string placement,
            uint errorCode,
            string errorMessage,
            params IParameter[] parameters)
        {
            RewardData = rewardData;
            Placement = placement;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            Parameters = parameters;
        }
    }
}
