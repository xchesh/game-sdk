using GameSdk.Core.Loggers;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

namespace GameSdk.Services.InApp
{
    public partial class InAppService : IDetailedStoreListener
    {
        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            SystemLog.Log(IInAppService.TAG, "Initialization successful");

            Controller = controller;
            Extensions = extensions;

            IsInitialized = true;

            PurchasesInitialized?.Invoke(true, null);
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            OnInitializeFailed(error, "none");
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            SystemLog.LogError(IInAppService.TAG, $"Initialization failed: {error} - {message}");

            IsInitialized = false;

            PurchasesInitialized?.Invoke(false, message);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
        {
            SystemLog.Log(IInAppService.TAG, $"Purchase succeeded: {e.purchasedProduct.definition.id}");

            bool resultProcessed = false;

            foreach (var listener in ActiveListeners)
            {
                listener.OnProcessPurchase(e);

                resultProcessed = true;
            }

            if (!resultProcessed)
            {
                SystemLog.LogError(
                    IInAppService.TAG,
                    "Purchase not correctly processed for product \""
                    + e.purchasedProduct.definition.id
                    + "\". No listener handled the purchase."
                );
            }

            PurchaseSucceeded?.Invoke(e);

            // Always return Complete, otherwise the purchase will be considered failed.
            // Do not return Pending, as it will not be processed correctly.
            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
        {
            OnPurchaseFailed(product, new PurchaseFailureDescription(product.definition.id, reason, "none"));
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription description)
        {
            SystemLog.LogError(IInAppService.TAG, $"Purchase failed: {product.definition.id} - {description}");

            bool resultProcessed = false;

            foreach (var listener in ActiveListeners)
            {
                listener.OnPurchaseFailed(product, description);

                resultProcessed = true;
            }

            if (!resultProcessed)
            {
                SystemLog.LogError(
                    IInAppService.TAG,
                    "Purchase not correctly processed for product \""
                    + product.definition.id
                    + "\". No listener handled the purchase."
                );
            }

            PurchaseFailed?.Invoke(description);
        }
    }
}
