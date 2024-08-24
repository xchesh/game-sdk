using GameSdk.Sources.Core.Common;

namespace GameSdk.Sources.Core.Prices
{
    public struct PriceResultError : IPriceResult
    {
        public IPriceData PriceData { get; }
        public string Placement { get; }
        public IParameter[] Parameters { get; }

        public uint ErrorCode { get; }
        public string ErrorMessage { get; }

        public PriceResultError(
            IPriceData priceData,
            string placement,
            uint errorCode,
            string errorMessage,
            params IParameter[] parameters)
        {
            PriceData = priceData;
            Placement = placement;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            Parameters = parameters;
        }
    }
}
