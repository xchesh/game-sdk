using System;
using Cysharp.Threading.Tasks;
using GameSdk.Core.Common;

namespace GameSdk.Core.Prices
{
    public interface IPrice
    {
        Type DataType { get; }

        bool CanPurchase(IPriceData data, params IParameter[] parameters);

        UniTask<IPriceResult> Purchase(IPriceData data, params IParameter[] parameters);
    }
}
