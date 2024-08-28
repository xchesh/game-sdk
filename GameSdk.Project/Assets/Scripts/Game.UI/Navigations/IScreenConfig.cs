using System;
using UnityEngine.UIElements;

namespace Game.UI.Navigations
{
    public interface IScreenConfig
    {
        Type Type { get; }
        bool IsLazyLoad { get; }
        VisualTreeAsset Asset { get; }
    }
}
