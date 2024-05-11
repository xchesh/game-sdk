namespace GameSdk.Core.Prices.PurchaseResults
{
    public struct PurchaseResultSuccess : IPurchaseResult
    {
        public readonly IPriceData PriceData { get; }
        public readonly string Placement { get; }

        public PurchaseResultSuccess(IPriceData priceData, string placement)
        {
            PriceData = priceData;
            Placement = placement;
        }
    }
}