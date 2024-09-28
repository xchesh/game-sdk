using UnityEngine.UIElements;

namespace Navigation
{
    [UxmlElement("Screen")]
    public abstract partial class ScreenElement : VisualElement
    {
        public ScreenElement Parent { get; private set; }

        public ScreenElement()
        {
            RegisterCallback<AttachToPanelEvent>(OnAttachToPanel);
            RegisterCallback<DetachFromPanelEvent>(OnDetachFromPanel);
        }

        private void OnAttachToPanel(AttachToPanelEvent evt)
        {
            Parent = this.GetFirstAncestorOfType<ScreenElement>();
        }

        private void OnDetachFromPanel(DetachFromPanelEvent evt)
        {
            Parent = null;
        }
    }
}
