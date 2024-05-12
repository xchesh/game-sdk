namespace GameSdk.Core.Prices
{
    public struct PurchaseResultError : IPurchaseResult
    {
        public IPriceData PriceData { get; }
        public string Placement { get; }

        public uint ErrorCode { get; }
        public string ErrorMessage { get; }

        public PurchaseResultError(IPriceData priceData, string placement, uint errorCode, string errorMessage)
        {
            PriceData = priceData;
            Placement = placement;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }
}