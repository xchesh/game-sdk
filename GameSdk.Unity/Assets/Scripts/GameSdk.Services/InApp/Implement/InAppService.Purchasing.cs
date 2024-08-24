using GameSdk.Sources.Core.Loggers;
using UnityEngine;
using UnityEngine.Purchasing;

namespace GameSdk.Services.InApp
{
    public partial class InAppService
    {
        // Copied from CodelessIAPStoreListener.InitializePurchasing();
        private void InitializePurchasing()
        {
            Catalog = ProductCatalog.LoadDefaultCatalog();

            var module = StandardPurchasingModule.Instance();
            module.useFakeStoreUIMode = FakeStoreUIMode.StandardUser;

            var builder = ConfigurationBuilder.Instance(module);

            IAPConfigurationHelper.PopulateConfigurationBuilder(ref builder, Catalog);
            Builder = builder;

            UnityPurchasing.Initialize(this, builder);
        }

        // Copied from BaseIAPButton.Restore();
        private void RestoreTransactions()
        {
            if (Application.platform == RuntimePlatform.WSAPlayerX86 ||
                Application.platform == RuntimePlatform.WSAPlayerX64 ||
                Application.platform == RuntimePlatform.WSAPlayerARM)
            {
                Extensions.GetExtension<IMicrosoftExtensions>().RestoreTransactions();
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer ||
                     Application.platform == RuntimePlatform.OSXPlayer ||
                     Application.platform == RuntimePlatform.tvOS
#if UNITY_VISIONOS
                         || Application.platform == RuntimePlatform.VisionOS
#endif
                    )
            {
                Extensions.GetExtension<IAppleExtensions>().RestoreTransactions(OnPurchasesRestored);
            }
            else if (Application.platform == RuntimePlatform.Android &&
                     StandardPurchasingModule.Instance().appStore == AppStore.GooglePlay)
            {
                Extensions.GetExtension<IGooglePlayStoreExtensions>().RestoreTransactions(OnPurchasesRestored);
            }
            else
            {
                SystemLog.LogWarning(IInAppService.TAG, Application.platform + " is not a supported platform");
            }
        }

        // Copied from CodelessIAPStoreListener.HasProductInCatalog(string productID);
        private bool HasProductInCatalog(string productID)
        {
            foreach (var product in Catalog.allProducts)
            {
                if (product.id == productID)
                {
                    return true;
                }
            }

            SystemLog.LogError(IInAppService.TAG, "InAppService attempted to get unknown product " + productID);

            return false;
        }

        private void OnPurchasesRestored(bool success, string error)
        {
            SystemLog.Log(IInAppService.TAG, $"Purchases restored: {success} - {error}");

            bool processed = false;

            foreach (var listener in ActiveListeners)
            {
                listener.OnPurchasesRestored(success, error);

                processed = true;
            }

            if (!processed)
            {
                SystemLog.LogError(
                    IInAppService.TAG,
                    "Purchases not correctly processed. No listener handled the purchases."
                );
            }

            PurchasesRestored?.Invoke(success, error);
        }
    }
}
