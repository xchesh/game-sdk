using GameSdk.Sources.Core.Common;

namespace GameSdk.Sources.Core.Prices
{
    public interface IPriceResult
    {
        IPriceData PriceData { get; }
        string Placement { get; }
        IParameter[] Parameters { get; }
    }
}
