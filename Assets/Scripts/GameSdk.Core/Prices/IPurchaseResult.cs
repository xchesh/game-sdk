namespace GameSdk.Core.Prices
{
    public interface IPurchaseResult
    {
        IPriceData PriceData { get; }
        string Placement { get; }
    }
}