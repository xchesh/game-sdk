using System;
using System.Collections.Generic;
using GameSdk.Core.Loggers;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

namespace GameSdk.Services.InApp
{
    public partial class InAppService : IInAppService
    {
        public event Action<bool, string> PurchasesInitialized;
        public event Action<bool, string> PurchasesRestored;

        public event Action<PurchaseEventArgs> PurchaseSucceeded;
        public event Action<PurchaseFailureDescription> PurchaseFailed;

        public ProductCatalog Catalog { get; private set; }
        public ConfigurationBuilder Builder { get; private set; }
        public IStoreController Controller { get; private set; }
        public IExtensionProvider Extensions { get; private set; }
        public IList<IInAppListener> ActiveListeners { get; }

        public bool IsInitialized { get; private set; }

        public InAppService(IEnumerable<IInAppListener> listeners)
        {
            ActiveListeners = new List<IInAppListener>(listeners);

            if (ActiveListeners == null)
            {
                SystemLog.LogWarning(IInAppService.TAG, "InAppService has no listeners");
            }
        }

        public void Initialize()
        {
            InitializePurchasing();
        }

        public void RestorePurchases()
        {
            RestoreTransactions();
        }

        public bool HasProduct(string productId)
        {
            return HasProductInCatalog(productId);
        }

        public Product GetProduct(string productId)
        {
            if (Controller?.products != null && !string.IsNullOrEmpty(productId))
            {
                return Controller.products.WithID(productId);
            }

            SystemLog.LogError(IInAppService.TAG, "InAppService attempted to get unknown product " + productId);

            return null;
        }

        public void PurchaseProduct(string productId)
        {
            if (Controller == null)
            {
                SystemLog.LogError(IInAppService.TAG,
                    "Purchase failed because Purchasing was not initialized correctly");

                return;
            }

            Controller.InitiatePurchase(productId);
        }

        public string GetSafePriceString(string productId)
        {
            var product = GetProduct(productId);

            if (product != null)
            {
                return product.metadata.localizedPriceString;
            }

            return IInAppService.BUY;
        }
    }
}