using System.Collections.Generic;
using UnityEngine.Pool;

namespace GameSdk.UI
{
    public partial class Navigation
    {
        private Stack<HistoryItem> _history = new();
        private HistoryItem HistoryLast => _history.Peek();

        private HistoryItem CreateHistoryItem(Screen screen, object data)
        {
            var instance = GenericPool<HistoryItem>.Get();

            instance.Element = screen;
            instance.Data = data;

            return instance;
        }

        private void ReleaseHistoryItem(HistoryItem instance)
        {
            instance.Element = null;
            instance.Data = null;

            GenericPool<HistoryItem>.Release(instance);
        }

        private class HistoryItem
        {
            public Screen Element { get; set; }
            public object Data { get; set; }
        }
    }
}
