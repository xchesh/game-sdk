using GameSdk.Core.Parameters;

namespace GameSdk.Core.Prices
{
    public interface IPriceResult
    {
        IPriceData PriceData { get; }
        string Placement { get; }
        IParameter[] Parameters { get; }
    }
}