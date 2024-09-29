using UnityEngine.UIElements;
using GameSdk.UI;
using System;

namespace Screens
{
    [UxmlElement("MainScreen")]
    public partial class MainScreen : Screen
    {
        [UxmlAttribute]
        public string SettingsButtonName { get; set; } = "Settings";

        private VisualElement _settingsButton;

        private Navigation _navigation;

        public MainScreen() : base()
        {
            RegisterCallback<AttachToPanelEvent>(OnAttachToPanel);
            RegisterCallback<DetachFromPanelEvent>(OnDetachFromPanel);
        }

        private void OnAttachToPanel(AttachToPanelEvent evt)
        {
            _navigation = this.GetFirstAncestorOfType<Navigation>();
            _settingsButton = this.Q<Button>(SettingsButtonName);

            if (_settingsButton != null)
            {
                _settingsButton.RegisterCallback<ClickEvent>(OnSettingsButtonClick);
            }
        }

        private void OnDetachFromPanel(DetachFromPanelEvent evt)
        {
            if (_settingsButton != null)
            {
                _settingsButton.UnregisterCallback<ClickEvent>(OnSettingsButtonClick);
            }

            _settingsButton = null;
            _navigation = null;
        }

        private void OnSettingsButtonClick(ClickEvent evt)
        {
            _navigation?.Push<SettingsScreen>();
        }
    }
}
