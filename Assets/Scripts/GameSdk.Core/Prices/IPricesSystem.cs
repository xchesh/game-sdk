using Cysharp.Threading.Tasks;
using GameSdk.Core.Common;
using GameSdk.Core.Essentials;

namespace GameSdk.Core.Prices
{
    public interface IPricesSystem
    {
        internal InstancesManager<IPrice> Manager { get; }

        bool CanPurchase(IPriceData priceData, params IParameter[] parameters);

        UniTask<IPriceResult> Purchase(IPriceData priceData, params IParameter[] parameters);
    }
}