namespace GameSdk.Core.Prices.PurchaseResults
{
    public struct PurchaseResultError : IPurchaseResult
    {
        public readonly IPriceData PriceData { get; }
        public readonly string Placement { get; }

        public readonly uint ErrorCode { get; }
        public readonly string ErrorMessage { get; }

        public PurchaseResultError(IPriceData priceData, string placement, uint errorCode, string errorMessage)
        {
            PriceData = priceData;
            Placement = placement;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }
}