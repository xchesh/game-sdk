using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameSdk.Core.Essentials;
using GameSdk.Core.Parameters;

namespace GameSdk.Core.Prices
{
    public class PricesSystem : IPricesSystem
    {
        private readonly InstancesManager<IPrice> _manager = new();

        InstancesManager<IPrice> IPricesSystem.Manager => _manager;

        public PricesSystem(IEnumerable<IPrice> prices)
        {
            foreach (var price in prices)
            {
                _manager.Register(price.DataType, price);

                if (price is IPrice.IWithSystem withSystem)
                {
                    withSystem.SetSystem(this);
                }
            }
        }

        public bool CanPurchase(IPriceData priceData, params IParameter[] parameters)
        {
            return priceData != null && _manager.Get(priceData.GetType()).CanPurchase(priceData, parameters);
        }

        public UniTask<IPurchaseResult> Purchase(IPriceData priceData, params IParameter[] parameters)
        {
            return _manager.Get(priceData.GetType()).Purchase(priceData, parameters);
        }
    }
}
