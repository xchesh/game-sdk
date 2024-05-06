using UnityEngine.Purchasing;

namespace GameSdk.Services.InApp
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
