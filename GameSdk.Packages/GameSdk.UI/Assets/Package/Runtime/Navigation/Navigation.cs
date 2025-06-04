using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace GameSdk.UI
{
    [Preserve, UxmlElement("Navigation")]
    public partial class Navigation : VisualElement
    {
        [UxmlAttribute, UxmlTypeReference(typeof(Screen))]
        public Type DefaultScreen { get; set; }

        [UxmlAttribute]
        public bool AutoActivate { get; set; } = true;

        [UxmlAttribute]
        public string ScreenClass { get; set; } = "navigation-screen";

        [UxmlAttribute]
        public string ScreenClassActive { get; set; } = "navigation-screen__active";

        private Dictionary<Type, Screen> _screens = new();

        [Preserve]
        public Navigation()
        {
            RegisterCallback<AttachToPanelEvent>(OnAttachToPanelEvent);
            RegisterCallback<DetachFromPanelEvent>(OnDetachFromPanelEvent);
        }

        private void OnAttachToPanelEvent(AttachToPanelEvent evt)
        {
            _screens = new Dictionary<Type, Screen>();

            this.Query<Screen>().ForEach((screen) =>
            {
                screen.Attached += OnScreenAttached;

                _screens.Add(screen.GetType(), null);
            });
        }

        private void OnDetachFromPanelEvent(DetachFromPanelEvent evt)
        {
            foreach (var screen in _screens)
            {
                if (screen.Value != null)
                {
                    screen.Value.Attached -= OnScreenAttached;
                }
            }

            _screens = null;
        }

        private void OnScreenAttached(AttachToPanelEvent evt, Screen screenElement)
        {
            _screens[screenElement.GetType()] = screenElement;

            var screen = GetScreenElement(screenElement);

            if (screen.ClassListContains(ScreenClass) is false)
            {
                screen.AddToClassList(ScreenClass);
            }

            OnAfterScreenAttached();
        }

        private void OnAfterScreenAttached()
        {
            if (AutoActivate && _screens.Values.All(screen => screen != null))
            {
                if (_screens.ContainsKey(DefaultScreen) is false)
                {
                    UnityEngine.Debug.LogWarning("Default screen not found");
                }

                foreach (var screen in _screens)
                {
                    HideScreen(screen.Value);
                }

                var chain = GetScreenChain(_screens[DefaultScreen]);

                foreach (var screen in chain)
                {
                    Push(screen);
                }
            }
        }

        private List<Screen> GetScreenChain(Screen screen)
        {
            var chain = new List<Screen> { screen };

            while (screen.Parent != null)
            {
                chain.Add(screen.Parent);
                screen = screen.Parent;
            }

            chain.Reverse();

            return chain;
        }

        private void BlurScreen(Screen screen)
        {
            if (screen == null)
            {
                return;
            }

            var screenElement = GetScreenElement(screen);

            screenElement.SetEnabled(false);
        }

        private void ShowScreen(Screen screen)
        {
            while (true)
            {
                var screenElement = GetScreenElement(screen);

                screenElement.SetEnabled(true);
                screenElement.AddToClassList(ScreenClassActive);
                screenElement.BringToFront();

                if (screen.Parent != null)
                {
                    screen = screen.Parent;
                    continue;
                }

                break;
            }
        }

        private void HideScreen(Screen screen)
        {
            if (screen == null)
            {
                return;
            }

            var screenElement = GetScreenElement(screen);

            screenElement.RemoveFromClassList(ScreenClassActive);
            screenElement.SetEnabled(false);
        }
    }
}
