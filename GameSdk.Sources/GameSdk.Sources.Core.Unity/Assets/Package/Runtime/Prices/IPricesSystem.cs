using Cysharp.Threading.Tasks;
using GameSdk.Sources.Core.Common;
using GameSdk.Sources.Core.Essentials;

namespace GameSdk.Sources.Core.Prices
{
    public interface IPricesSystem
    {
        internal InstancesManager<IPrice> Manager { get; }

        bool CanPurchase(IPriceData priceData, params IParameter[] parameters);

        UniTask<IPriceResult> Purchase(IPriceData priceData, params IParameter[] parameters);
    }
}
