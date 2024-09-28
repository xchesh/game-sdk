using System;
using System.Collections.Generic;
using Navigation;
using UnityEngine.UIElements;

namespace Navbar
{
    [UxmlElement("Navbar")]
    public partial class NavbarElement : VisualElement
    {
        [UxmlAttribute]
        public string ActiveButtonClass { get; set; } = "navbar-button__active";

        private NavigationElement _navigation;
        private List<NavbarButtonElement> _buttons = new List<NavbarButtonElement>();

        public NavbarElement()
        {
            RegisterCallback<AttachToPanelEvent>(OnAttachToPanel);
            RegisterCallback<DetachFromPanelEvent>(OnDetachFromPanel);
        }

        private void OnAttachToPanel(AttachToPanelEvent evt)
        {
            _buttons = this.Query<NavbarButtonElement>().ToList();
            _navigation = this.GetFirstAncestorOfType<NavigationElement>();

            foreach (var button in _buttons)
            {
                button.RegisterCallback<ClickEvent, NavbarButtonElement>(OnButtonClicked, button);
            }
        }

        private void OnDetachFromPanel(DetachFromPanelEvent evt)
        {
            foreach (var button in _buttons)
            {
                button.UnregisterCallback<ClickEvent, NavbarButtonElement>(OnButtonClicked);
            }

            _navigation = null;
            _buttons = null;
        }

        private void OnButtonClicked(ClickEvent evt, NavbarButtonElement btn)
        {
            foreach (var button in _buttons)
            {
                button.RemoveFromClassList(ActiveButtonClass);
            }

            btn.AddToClassList(ActiveButtonClass);

            _navigation?.Show(btn.TargetScreen);
        }
    }
}
