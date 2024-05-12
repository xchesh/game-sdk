namespace GameSdk.Core.Prices
{
    public struct PurchaseResultSuccess : IPurchaseResult
    {
        public IPriceData PriceData { get; }
        public string Placement { get; }

        public PurchaseResultSuccess(IPriceData priceData, string placement)
        {
            PriceData = priceData;
            Placement = placement;
        }
    }
}