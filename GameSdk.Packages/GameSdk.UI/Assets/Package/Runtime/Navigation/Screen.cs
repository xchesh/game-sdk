using System;
using UnityEngine.UIElements;

namespace GameSdk.UI
{
    [UxmlElement("Screen")]
    public abstract partial class Screen : VisualElement
    {
        public Screen Parent { get; private set; }

        public event Action<AttachToPanelEvent, Screen> Attached;

        public Screen()
        {
            RegisterCallback<AttachToPanelEvent>(OnAttachToPanel);
            RegisterCallback<DetachFromPanelEvent>(OnDetachFromPanel);
        }

        private void OnAttachToPanel(AttachToPanelEvent evt)
        {
            Parent = GetFirstAncestorOfType<Screen>();

            Attached?.Invoke(evt, this);
        }

        private void OnDetachFromPanel(DetachFromPanelEvent evt)
        {
            Parent = null;
        }

        internal void SetData(object data)
        {
            OnDataChanged(data);
        }

        protected virtual void OnDataChanged(object data) { }
    }
}
