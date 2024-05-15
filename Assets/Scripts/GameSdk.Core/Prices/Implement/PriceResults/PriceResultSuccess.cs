using GameSdk.Core.Common;

namespace GameSdk.Core.Prices
{
    public struct PriceResultSuccess : IPriceResult
    {
        public IPriceData PriceData { get; }
        public string Placement { get; }
        public IParameter[] Parameters { get; }

        public PriceResultSuccess(IPriceData priceData, string placement, params IParameter[] parameters)
        {
            PriceData = priceData;
            Placement = placement;
            Parameters = parameters;
        }
    }
}