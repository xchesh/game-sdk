using UnityEngine.UIElements;
using GameSdk.UI;

namespace Screens
{
    [UxmlElement("SettingsScreen")]
    public partial class SettingsScreen : Screen
    {
        [UxmlAttribute]
        public string CloseButtonName { get; set; } = "Close";

        private VisualElement _closeButton;

        private Navigation _navigation;

        public SettingsScreen() : base()
        {
            RegisterCallback<AttachToPanelEvent>(OnAttachToPanel);
            RegisterCallback<DetachFromPanelEvent>(OnDetachFromPanel);
        }

        private void OnAttachToPanel(AttachToPanelEvent evt)
        {
            _navigation = this.GetFirstAncestorOfType<Navigation>();
            _closeButton = this.Q<Button>(CloseButtonName);

            if (_closeButton != null)
            {
                _closeButton.RegisterCallback<ClickEvent>(OnCloseButtonClick);
            }
        }

        private void OnDetachFromPanel(DetachFromPanelEvent evt)
        {
            if (_closeButton != null)
            {
                _closeButton.UnregisterCallback<ClickEvent>(OnCloseButtonClick);
            }

            _closeButton = null;
            _navigation = null;
        }

        private void OnCloseButtonClick(ClickEvent evt)
        {
            _navigation?.Pop();
        }
    }
}
