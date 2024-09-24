using System.Collections.Generic;
using UnityEngine.UIElements;

namespace GameSdk.UI.Navigation
{
    public partial class Navigation : INavigation
    {
        private readonly Stack<IScreen> _stack;
        private readonly IScreenFactory _screenFactory;
        private readonly IDictionary<System.Type, IScreen> _screens;

        public IScreen Current { get; private set; }
        public INavigation Parent { get; private set; }
        public INavigationConfig Config { get; private set; }
        public NavigationComponent Component { get; private set; }

        public Navigation(IScreenFactory navigationScreenFactory)
        {
            _stack = new Stack<IScreen>();
            _screens = new Dictionary<System.Type, IScreen>();

            _screenFactory = navigationScreenFactory;
        }

        public void Initialize(INavigationConfig navigationConfig, VisualElement visualElement, INavigation parent = null)
        {
            Parent = parent;
            Config = navigationConfig;
            Component = visualElement as NavigationComponent;

            if (Component == null)
            {
                throw new System.ArgumentException($"Navigation component must be of type {nameof(NavigationComponent)}");
            }

            if (Config == null)
            {
                throw new System.ArgumentException($"Navigation config must be of type {nameof(INavigationConfig)}");
            }

            // Initialize the navigation non-lazy screens
            foreach (var screenConfig in Config.Screens)
            {
                if (screenConfig.IsLazyLoad is false)
                {
                    CreateScreen(screenConfig.Type);
                }
            }
        }

        public void Pop()
        {
            // When the stack is empty, try to pop the parent stack
            if (_stack.Count < 1)
            {
                Parent?.Pop();

                return;
            }

            // Hide the current screen and remove it from stack
            HideScreen(_stack.Pop());

            // Show the last screen from stack as the current screen if it's not empty
            if (_stack.Count > 0)
            {
                ShowScreen(_stack.Peek());

                return;
            }

            // When the stack is empty, try to pop the parent stack
            Parent?.Pop();
        }

        public T PopTo<T>() where T : IScreen
        {
            var type = typeof(T);
            var count = Parent == null ? 1 : 0;

            while (_stack.Count > count)
            {
                // Peek the last screen from stack and check if it's the current type
                var screen = _stack.Peek();

                // Break the loop if it's the current type
                if (screen.GetType() == type)
                {
                    break;
                }

                // Hide the screen and remove it from stack
                // if it's not the current type
                HideScreen(_stack.Pop());
            }

            // When the stack is empty, try to pop the parent stack
            if (_stack.Count < 1 && Parent != null)
            {
                return Parent.PopTo<T>();
            }

            // Show the last screen from stack as the current screen
            return ShowScreen((T)_stack.Peek());
        }

        public T Push<T>(params object[] parameters) where T : IScreen
        {
            return Push(GetScreen<T>());
        }

        public T Push<T>(T screen, params object[] parameters) where T : IScreen
        {
            if (Config.HasScreenConfig<T>() is false)
            {
                throw new System.ArgumentException($"Screen {typeof(T).Name} is not configured in the navigation config");
            }

            if (_stack.Count > 0)
            {
                BlurScreen_Action(Current);
            }

            _stack.Push(screen);

            return ShowScreen((T)screen, parameters);
        }

        public T Replace<T>(params object[] parameters) where T : IScreen
        {
            return Replace(GetScreen<T>());
        }

        public T Replace<T>(T screen, params object[] parameters) where T : IScreen
        {
            if (Config.HasScreenConfig<T>() is false)
            {
                throw new System.ArgumentException($"Screen {typeof(T).Name} is not configured in the navigation config");
            }

            if (_stack.Count > 0)
            {
                HideScreen(_stack.Pop());
            }

            _stack.Push(screen);

            return ShowScreen((T)screen, parameters);
        }
    }
}
