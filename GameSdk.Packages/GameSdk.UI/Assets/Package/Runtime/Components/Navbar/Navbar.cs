using System.Collections.Generic;
using UnityEngine.UIElements;

namespace GameSdk.UI
{
    [UxmlElement("Navbar")]
    public partial class Navbar : VisualElement
    {
        [UxmlAttribute]
        public string ActiveButtonClass { get; set; } = "navbar-button__active";

        private Navigation _navigation;
        private List<NavbarButton> _buttons = new List<NavbarButton>();

        public Navbar()
        {
            RegisterCallback<AttachToPanelEvent>(OnAttachToPanel);
            RegisterCallback<DetachFromPanelEvent>(OnDetachFromPanel);
        }

        private void OnAttachToPanel(AttachToPanelEvent evt)
        {
            _buttons = this.Query<NavbarButton>().ToList();
            _navigation = this.GetFirstAncestorOfType<Navigation>();

            if (_navigation != null)
            {
                _navigation.ScreenChanged += OnScreenChanged;
            }

            foreach (var button in _buttons)
            {
                button.RegisterCallback<ClickEvent, NavbarButton>(OnButtonClicked, button);
            }
        }

        private void OnDetachFromPanel(DetachFromPanelEvent evt)
        {
            foreach (var button in _buttons)
            {
                button.UnregisterCallback<ClickEvent, NavbarButton>(OnButtonClicked);
            }

            if (_navigation != null)
            {
                _navigation.ScreenChanged -= OnScreenChanged;
            }

            _navigation = null;
            _buttons = null;
        }

        private void OnButtonClicked(ClickEvent evt, NavbarButton btn)
        {
            var targetScreen = _navigation?.GetScreen(btn.TargetScreen);

            // Hide screen if pressed to the same screen
            if (targetScreen == _navigation?.CurrentScreen)
            {
                _navigation?.Pop();

                return;
            }

            // Replace screen if pressed to the screen in the same parent - neighbor screen
            if (_navigation?.CurrentScreen.Parent == targetScreen.Parent)
            {
                _navigation?.Replace(targetScreen);

                return;
            }

            // Push screen if pressed to the screen in the different parent
            _navigation?.Push(targetScreen);
        }

        private void OnScreenChanged(Screen screen)
        {
            foreach (var button in _buttons)
            {
                if (button.TargetScreen == screen.GetType())
                {
                    button.AddToClassList(ActiveButtonClass);
                }
                else
                {
                    button.RemoveFromClassList(ActiveButtonClass);
                }
            }
        }
    }
}
