using UnityEngine.Purchasing;

namespace GameSdk.Sources.Services.InApp
{
    public struct InAppPurchaseSuccess : IInAppPurchaseResult
    {
        public readonly PurchaseEventArgs EventArgs;

        public InAppPurchaseSuccess(PurchaseEventArgs eventArgs)
        {
            EventArgs = eventArgs;
        }
    }
}
