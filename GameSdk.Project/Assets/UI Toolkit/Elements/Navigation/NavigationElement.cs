using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

namespace Navigation
{
    [UxmlElement("Navigation")]
    public partial class NavigationElement : VisualElement
    {
        private class Screen
        {
            public ScreenElement Element { get; set; }
            public bool IsAttached { get; set; }
        }

        [UxmlAttribute("config")]
        public NavigationConfig Config { get; set; }

        [UxmlAttribute, UxmlTypeReference(typeof(ScreenElement))]
        public Type DefaultScreen { get; set; }

        [UxmlAttribute]
        public string ActiveButtonClass { get; set; } = "navigation-screen__active";

        private Dictionary<Type, Screen> _screens = new Dictionary<Type, Screen>();

        private ScreenElement _currentScreen;

        public NavigationElement()
        {
            RegisterCallback<AttachToPanelEvent>(OnAttachToPanelEvent);
            RegisterCallback<DetachFromPanelEvent>(OnDetachFromPanelEvent);
        }

        private void OnAttachToPanelEvent(AttachToPanelEvent evt)
        {
            _screens = new Dictionary<Type, Screen>();

            foreach (var screen in this.Query<ScreenElement>().ToList())
            {
                screen.RegisterCallback<AttachToPanelEvent, ScreenElement>(OnScreenAttached, screen);

                _screens.Add(screen.GetType(), new Screen { Element = screen, IsAttached = false });
            }
        }

        private void OnScreenAttached(AttachToPanelEvent evt, ScreenElement screenElement)
        {
            if (_screens.TryGetValue(screenElement.GetType(), out var screenState))
            {
                screenState.IsAttached = true;
            }

            OnAfterScreenAttached();
        }

        private void OnDetachFromPanelEvent(DetachFromPanelEvent evt)
        {
            foreach (var screen in _screens)
            {
                screen.Value.Element.UnregisterCallback<AttachToPanelEvent, ScreenElement>(OnScreenAttached);
            }

            _screens = null;
        }

        private void OnAfterScreenAttached()
        {
            if (_screens.Values.All(screen => screen.IsAttached))
            {
                foreach (var screen in _screens)
                {
                    HideScreen(screen.Value.Element);
                }

                Show(DefaultScreen);
            }
        }

        public void Show(Type screenType)
        {
            if (screenType == null)
            {
                return;
            }

            foreach (var screen in _screens)
            {
                var screenElement = screen.Value.Element;

                if (screenElement.GetType() == screenType)
                {
                    _currentScreen = screenElement;
                }
                else
                {
                    HideScreen(screenElement);
                }
            }

            ShowScreen(_currentScreen);
        }

        private VisualElement GetScreenElement(ScreenElement screen)
        {
            return screen?.parent is TemplateContainer ? screen.parent : screen;
        }

        private void ShowScreen(ScreenElement screen)
        {
            if (screen == null)
            {
                return;
            }

            var screenElement = GetScreenElement(screen);

            screenElement.AddToClassList(ActiveButtonClass);

            if (screen.Parent != null)
            {
                ShowScreen(screen.Parent);
            }
        }

        private void HideScreen(ScreenElement screen)
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
