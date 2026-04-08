using GameSdk.Core.Common;
using GameSdk.Core.Essentials;
using UnityEngine;

namespace GameSdk.Core.Prices
{
    public interface IPricesSystem
    {
        internal InstancesManager<IPrice> Manager { get; }

        bool CanPurchase(IPriceData priceData, params IParameter[] parameters);

        Awaitable<IPriceResult> Purchase(IPriceData priceData, params IParameter[] parameters);
    }
}
