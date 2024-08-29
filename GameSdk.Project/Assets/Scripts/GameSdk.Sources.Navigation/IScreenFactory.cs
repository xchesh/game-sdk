using System;
using UnityEngine.UIElements;

namespace GameSdk.Sources.Navigation
{
    public interface IScreenFactory
    {
        IScreen Create(Type type, INavigation navigation, VisualElement parent);
        T Create<T>(INavigation navigation, VisualElement parent) where T : IScreen;
        void Destroy<T>(T screen) where T : IScreen;
    }
}
