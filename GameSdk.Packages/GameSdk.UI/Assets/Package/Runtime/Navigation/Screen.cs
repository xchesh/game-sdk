using System;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace GameSdk.UI
{
    [Preserve, UxmlElement("Screen")]
    public abstract partial class Screen : DataSourceElement
    {
        public Screen Parent { get; protected set; }
        public Navigation Navigation { get; protected set; }
        public VisualElement Element => parent is TemplateContainer ? parent : this;

        public event Action<AttachToPanelEvent, Screen> Attached;

        [Preserve]
        public Screen()
        {
            RegisterCallback<AttachToPanelEvent>(OnAttachToPanel);
            RegisterCallback<DetachFromPanelEvent>(OnDetachFromPanel);
        }

        private void OnAttachToPanel(AttachToPanelEvent evt)
        {
            Navigation = GetFirstAncestorOfType<Navigation>();
            Parent = GetFirstAncestorOfType<Screen>();

            Attached?.Invoke(evt, this);
        }

        private void OnDetachFromPanel(DetachFromPanelEvent evt)
        {
            Parent = null;
            Navigation = null;
        }


        internal void SetData(object data)
        {
            OnDataChanged(data);
        }

        protected virtual void OnDataChanged(object data)
        {
        }

        protected internal virtual bool OnCancel()
        {
            return true;
        }
    }

    public abstract partial class Screen<T> : Screen
    {
        public T Data { get; protected set; }

        protected override void OnDataChanged(object data)
        {
            if (data is T typedData)
            {
                Data = typedData;
                OnDataChanged();

                return;
            }

            throw new InvalidCastException($"Cannot cast {data.GetType()} to {typeof(T)}");
        }

        protected abstract void OnDataChanged();
    }
}
