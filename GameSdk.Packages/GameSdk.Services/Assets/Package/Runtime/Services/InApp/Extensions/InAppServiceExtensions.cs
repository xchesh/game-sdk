using Cysharp.Threading.Tasks;
using GameSdk.Core.Loggers;
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

        public static UniTask<bool> InitializeAsync(this IInAppService inAppService)
        {
            var tcs = new UniTaskCompletionSource<bool>();

            inAppService.PurchasesInitialized += OnPurchasesInitialized;

            inAppService.Initialize();

            return tcs.Task;

            void OnPurchasesInitialized(bool success, string error)
            {
                inAppService.PurchasesInitialized -= OnPurchasesInitialized;

                tcs.TrySetResult(success);
            }
        }

        public static UniTask<IInAppPurchaseResult> PurchaseProductAsync(this IInAppService inAppService,
            string productId)
        {
            var tcs = new UniTaskCompletionSource<IInAppPurchaseResult>();

            inAppService.PurchaseSucceeded += OnPurchaseSucceeded;
            inAppService.PurchaseFailed += OnPurchaseFailed;

            inAppService.PurchaseProduct(productId);

            return tcs.Task;

            void OnPurchaseSucceeded(PurchaseEventArgs e)
            {
                if (e.purchasedProduct.definition.id != productId)
                {
                    SystemLog.LogError(IInAppService.TAG,
                        "Purchase succeeded for wrong product: " + e.purchasedProduct.definition.id);

                    return;
                }

                inAppService.PurchaseSucceeded -= OnPurchaseSucceeded;
                inAppService.PurchaseFailed -= OnPurchaseFailed;

                tcs.TrySetResult(new InAppPurchaseSuccess(e));
            }

            void OnPurchaseFailed(PurchaseFailureDescription error)
            {
                if (error.productId != productId)
                {
                    SystemLog.LogError(IInAppService.TAG, "Purchase failed for wrong product: " + error.productId);

                    return;
                }

                inAppService.PurchaseSucceeded -= OnPurchaseSucceeded;
                inAppService.PurchaseFailed -= OnPurchaseFailed;

                tcs.TrySetResult(new InAppPurchaseFailure(error));
            }
        }
    }
}
