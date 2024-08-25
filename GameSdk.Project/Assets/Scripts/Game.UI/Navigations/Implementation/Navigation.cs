using System.Collections.Generic;
using Game.UI.Navigations;
using GameSdk.Core.Common;
using UnityEngine.UIElements;

namespace GameSdk.UI.Navigations
{
    public class Navigation : INavigation
    {
        private readonly Stack<INavigationScreen> _stack = new Stack<INavigationScreen>();

        private NavigationComponent _component;

        public INavigationScreen Current { get; private set; }

        public void Initialize(VisualElement navigation)
        {
            _component = navigation as NavigationComponent;

            if (_component == null)
            {
                throw new System.ArgumentException($"Navigation component must be of type {nameof(NavigationComponent)}");
            }
        }

        public void Pop()
        {
            if (_stack.Count <= 1)
            {
                return;
            }

            var screen = _stack.Pop();

            screen.Hide();

            Current = _stack.Peek();
            Current.Show();
        }

        public T PopTo<T>(string name, params IParameter[] parameters) where T : INavigationScreen
        {
            while (_stack.Count > 1)
            {
                var screen = _stack.Pop();

                if (screen.GetType().Name == name)
                {
                    screen.Hide();

                    break;
                }

                screen.Hide();
            }

            Current = _stack.Peek();
            Current.Show();

            return (T)Current;
        }

        public T Push<T>(string name, params IParameter[] parameters) where T : INavigationScreen
        {
            throw new System.NotImplementedException();
        }
    }
}
