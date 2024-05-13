using Cysharp.Threading.Tasks;
using GameSdk.Core.Essentials;
using GameSdk.Core.Parameters;

namespace GameSdk.Core.Prices
{
    public interface IPricesSystem
    {
        internal InstancesManager<IPrice> Manager { get; }

        bool CanPurchase(IPriceData priceData, params IParameter[] parameters);

        UniTask<IPriceResult> Purchase(IPriceData priceData, params IParameter[] parameters);
    }
}