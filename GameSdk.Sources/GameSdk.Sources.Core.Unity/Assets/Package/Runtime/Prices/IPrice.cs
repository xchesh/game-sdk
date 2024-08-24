using System;
using Cysharp.Threading.Tasks;
using GameSdk.Sources.Core.Common;

namespace GameSdk.Sources.Core.Prices
{
    public interface IPrice
    {
        Type DataType { get; }

        bool CanPurchase(IPriceData data, params IParameter[] parameters);

        UniTask<IPriceResult> Purchase(IPriceData data, params IParameter[] parameters);
    }
}
