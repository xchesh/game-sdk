using GameSdk.Core.Common;
using GameSdk.Core.Essentials;
using UnityEngine;

namespace GameSdk.Core.Prices
{
    [JetBrains.Annotations.UsedImplicitly]
    public class PricesSystem : IPricesSystem
    {
        private readonly InstancesManager<IPrice> _manager = new();

        InstancesManager<IPrice> IPricesSystem.Manager => _manager;

        public bool CanPurchase(IPriceData priceData, params IParameter[] parameters)
        {
            return priceData != null && _manager.Get(priceData.GetType()).CanPurchase(priceData, parameters);
        }

        public Awaitable<IPriceResult> Purchase(IPriceData priceData, params IParameter[] parameters)
        {
            return _manager.Get(priceData.GetType()).Purchase(priceData, parameters);
        }
    }
}
