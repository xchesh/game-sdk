using System.Collections.Generic;

namespace GameSdk.Core.Prices
{
    public class PricesContext : IPricesContext
    {
        private readonly IEnumerable<IPrice> _prices;
        private readonly IPricesSystem _pricesSystem;

        public PricesContext(IEnumerable<IPrice> prices, IPricesSystem pricesSystem)
        {
            _prices = prices;
            _pricesSystem = pricesSystem;

            foreach (var price in _prices)
            {
                _pricesSystem.Manager.Register(price.DataType, price);

                if (price is IPrice.IWithSystem withSystem)
                {
                    withSystem.SetSystem(_pricesSystem);
                }
            }
        }

        public void Dispose()
        {
            foreach (var price in _prices)
            {
                _pricesSystem.Manager.Unregister(price.DataType);
            }
        }
    }
}