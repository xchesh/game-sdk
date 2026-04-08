using System;
using GameSdk.Core.Common;
using UnityEngine;

namespace GameSdk.Core.Prices
{
    public interface IPrice
    {
        Type DataType { get; }

        bool CanPurchase(IPriceData data, params IParameter[] parameters);

        Awaitable<IPriceResult> Purchase(IPriceData data, params IParameter[] parameters);
    }
}
