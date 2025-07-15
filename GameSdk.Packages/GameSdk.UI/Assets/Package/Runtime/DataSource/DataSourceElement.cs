using System;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

[Preserve]
public abstract partial class DataSourceElement : VisualElement
{
    private IVisualElementScheduledItem _schedule;

    [Preserve]
    public DataSourceElement()
    {
        RegisterCallback<AttachToPanelEvent>(OnAttachToPanel);
        RegisterCallback<DetachFromPanelEvent>(OnDetachFromPanel);
    }

    private void OnAttachToPanel(AttachToPanelEvent evt)
    {
        _schedule?.Pause();
        _schedule = schedule.Execute(OnDataSourceResolved).Every(10).Until(HasResolver);
    }

    private void OnDetachFromPanel(DetachFromPanelEvent evt)
    {
        _schedule?.Pause();
        _schedule = null;
    }

    private bool HasResolver()
    {
        return this.FindResolver() != null;
    }

    protected virtual void OnDataSourceResolved()
    {
        
    }
}