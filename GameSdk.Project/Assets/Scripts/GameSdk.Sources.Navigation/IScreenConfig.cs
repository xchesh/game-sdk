using System;
using UnityEngine.UIElements;

namespace GameSdk.Sources.Navigation
{
    public interface IScreenConfig
    {
        Type Type { get; }
        bool IsLazyLoad { get; }
        VisualTreeAsset Asset { get; }
    }
}
