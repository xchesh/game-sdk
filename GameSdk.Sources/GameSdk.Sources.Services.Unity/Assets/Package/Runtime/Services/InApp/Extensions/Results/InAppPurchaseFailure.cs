using UnityEngine.Purchasing.Extension;

namespace GameSdk.Sources.Services.InApp
{
    public struct InAppPurchaseFailure : IInAppPurchaseResult
    {
        public readonly PurchaseFailureDescription Description;

        public InAppPurchaseFailure(PurchaseFailureDescription description)
        {
            Description = description;
        }
    }
}
