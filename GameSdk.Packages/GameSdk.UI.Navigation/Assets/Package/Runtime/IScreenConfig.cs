using System;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

namespace GameSdk.UI.Navigation
{
    public interface IScreenConfig
    {
        Type Type { get; }
        bool IsLazyLoad { get; }
        AssetReferenceT<VisualTreeAsset> Asset { get; }
    }
}
