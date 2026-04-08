using GameSdk.Core.Loggers;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

namespace GameSdk.Services.InApp
{
    public static class InAppServiceExtensions
    {
        public static void AddListener(this IInAppService inAppService, IInAppListener listener)
        {
            if (listener != null)
            {
                inAppService.ActiveListeners.Add(listener);
            }
        }

        public static void RemoveListener(this IInAppService inAppService, IInAppListener listener)
        {
            if (listener != null)
            {
                inAppService.ActiveListeners.Remove(listener);
            }
        }

        public static async Awaitable<bool> InitializeAsync(this IInAppService inAppService)
        {
            var isCompleted = false;
            var isSuccess = false;

            inAppService.PurchasesInitialized += OnPurchasesInitialized;
            inAppService.Initialize();

            try
            {
                while (isCompleted is false)
                {
                    await Awaitable.NextFrameAsync();
                }

                return isSuccess;
            }
            finally
            {
                inAppService.PurchasesInitialized -= OnPurchasesInitialized;
            }

            void OnPurchasesInitialized(bool success, string error)
            {
                isSuccess = success;
                isCompleted = true;
            }
        }

        public static async Awaitable<IInAppPurchaseResult> PurchaseProductAsync(this IInAppService inAppService, string productId)
        {
            var isCompleted = false;
            IInAppPurchaseResult result = null;

            inAppService.PurchaseSucceeded += OnPurchaseSucceeded;
            inAppService.PurchaseFailed += OnPurchaseFailed;
            inAppService.PurchaseProduct(productId);

            try
            {
                while (isCompleted is false)
                {
                    await Awaitable.NextFrameAsync();
                }

                return result;
            }
            finally
            {
                inAppService.PurchaseSucceeded -= OnPurchaseSucceeded;
                inAppService.PurchaseFailed -= OnPurchaseFailed;
            }

            void OnPurchaseSucceeded(PurchaseEventArgs e)
            {
                if (e.purchasedProduct.definition.id != productId)
                {
                    SystemLog.LogError(IInAppService.TAG, "Purchase succeeded for wrong product: " + e.purchasedProduct.definition.id);
                    return;
                }

                result = new InAppPurchaseSuccess(e);
                isCompleted = true;
            }

            void OnPurchaseFailed(PurchaseFailureDescription error)
            {
                if (error.productId != productId)
                {
                    SystemLog.LogError(IInAppService.TAG, "Purchase failed for wrong product: " + error.productId);
                    return;
                }

                result = new InAppPurchaseFailure(error);
                isCompleted = true;
            }
        }
    }
}
