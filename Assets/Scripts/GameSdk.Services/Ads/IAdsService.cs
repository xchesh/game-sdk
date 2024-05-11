using Cysharp.Threading.Tasks;

namespace GameSdk.Services.Ads
{
    public interface IAdsService
    {
        event System.Action<string> InterstitialRequested;
        event System.Action<string, bool> InterstitialResult;

        event System.Action<string> RewardedRequested;
        event System.Action<string, bool> RewardedResult;

        bool IsBannerEnable { get; set; }

        bool IsInterstitialEnable { get; set; }
        bool IsInterstitialInProgress { get; }

        bool IsRewardedEnable { get; set; }
        bool IsRewardedInProgress { get; }

        UniTask Initialize();

        void SetAdsEnabled(bool state);

        bool IsInterstitialAvailable(string placement);

        bool IsRewardedAvailable(string placement);
        bool IsRewardedSkipAvailable(string placement);

        UniTask<bool> ShowInterstitial(string placement);
        UniTask<bool> ShowRewarded(string placement);

        void ShowBanner(string placement);
        void HideBanner();
    }
}