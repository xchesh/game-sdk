using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

namespace GameSdk.UI
{
    [UxmlElement("Navigation")]
    public partial class Navigation : VisualElement
    {
        [UxmlAttribute, UxmlTypeReference(typeof(Screen))]
        public Type DefaultScreen { get; set; }

        [UxmlAttribute]
        public string ActiveButtonClass { get; set; } = "navigation-screen__active";

        private Dictionary<Type, Screen> _screens = new();

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
                if(screen.Value != null)
                {
                    screen.Value.Attached -= OnScreenAttached;
                }
            }

            _screens = null;
        }

        private void OnScreenAttached(AttachToPanelEvent evt, Screen screenElement)
        {
            _screens[screenElement.GetType()] = screenElement;

            OnAfterScreenAttached();
        }

        private void OnAfterScreenAttached()
        {
            if (_screens.Values.All(screen => screen != null))
            {
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

        private VisualElement GetScreenElement(Screen screen)
        {
            return screen?.parent is TemplateContainer ? screen.parent : screen;
        }

        private void ShowScreen(Screen screen)
        {
            var screenElement = GetScreenElement(screen);

            screenElement.AddToClassList(ActiveButtonClass);
            screenElement.BringToFront();

            if (screen.Parent != null)
            {
                ShowScreen(screen.Parent);
            }
        }

        private void HideScreen(Screen screen)
        {
            if (screen == null)
            {
                return;
            }

            var screenElement = GetScreenElement(screen);

            screenElement.RemoveFromClassList(ActiveButtonClass);
        }
    }
}
