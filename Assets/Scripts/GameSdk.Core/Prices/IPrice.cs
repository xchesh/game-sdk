using System;
using Cysharp.Threading.Tasks;
using GameSdk.Core.Parameters;

namespace GameSdk.Core.Prices
{
    public interface IPrice
    {
        Type DataType { get; }

        bool CanPurchase(IPriceData data, params IParameter[] parameters);

        UniTask<IPurchaseResult> Purchase(IPriceData data, params IParameter[] parameters);

        public interface IWithSystem
        {
            IPricesSystem System { get; }

            void SetSystem(IPricesSystem system);
        }
    }
}