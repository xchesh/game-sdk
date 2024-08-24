using Cysharp.Threading.Tasks;
using GameSdk.Sources.Core.Common;
using GameSdk.Sources.Core.Essentials;

namespace GameSdk.Sources.Core.Prices
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

        public UniTask<IPriceResult> Purchase(IPriceData priceData, params IParameter[] parameters)
        {
            return _manager.Get(priceData.GetType()).Purchase(priceData, parameters);
        }
    }
}
