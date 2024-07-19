using GameSdk.Core.Common;
using UnityEngine.UIElements;

namespace Game.UI.Navigations
{
    public interface INavigation : INavigationActions
    {
        INavigationScreen Current { get; }

        void Initialize(VisualElement navigation);
    }
}
