using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

namespace GameSdk.Services.InApp
{
    public interface IInAppListener
    {
        void OnProcessPurchase(PurchaseEventArgs purchaseEventArgs);
        void OnPurchaseFailed(Product product, PurchaseFailureDescription description);
        void OnPurchasesRestored(bool success, string error);
    }
}
