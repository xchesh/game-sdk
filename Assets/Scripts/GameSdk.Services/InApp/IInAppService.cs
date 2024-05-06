using System;
using System.Collections.Generic;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

namespace GameSdk.Services.InApp
{
    public interface IInAppService
    {
        const string TAG = "IAP";
        const string BUY = "buy";

        event Action<bool, string> PurchasesInitialized;
        event Action<bool, string> PurchasesRestored;

        event Action<PurchaseEventArgs> PurchaseSucceeded;
        event Action<PurchaseFailureDescription> PurchaseFailed;

        ProductCatalog Catalog { get; }
        ConfigurationBuilder Builder { get; }

        IStoreController Controller { get; }
        IExtensionProvider Extensions { get; }

        IList<IInAppListener> ActiveListeners { get; }

        bool IsInitialized { get; }

        void Initialize();
        void RestorePurchases();

        bool HasProduct(string productId);
        Product GetProduct(string productId);
        void PurchaseProduct(string productId);

        string GetSafePriceString(string productId);
    }
}
