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
        _schedule = schedule.Execute(WaitDataSource).Every(10);
    }

    private void OnDetachFromPanel(DetachFromPanelEvent evt)
    {
        _schedule?.Pause();
        _schedule = null;
    }

    private void WaitDataSource()
    {
        if (this.FindResolver() != null)
        {
            _schedule?.Pause();
            _schedule = null;

            OnDataSourceResolved();
        }
    }

    protected virtual void OnDataSourceResolved()
    {

    }
}