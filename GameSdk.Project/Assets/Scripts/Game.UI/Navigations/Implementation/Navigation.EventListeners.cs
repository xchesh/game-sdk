using System.Collections.Generic;

namespace Game.UI.Navigations
{
    public partial class Navigation
    {
        private List<INavigationEventListener> _eventListeners = new();

        public void RegisterEventListener(INavigationEventListener eventListeners)
        {
            if (_eventListeners.Contains(eventListeners) is false)
            {
                _eventListeners.Add(eventListeners);
            }
        }

        public void UnregisterEventListener(INavigationEventListener eventListeners)
        {
            if (_eventListeners.Contains(eventListeners))
            {
                _eventListeners.Remove(eventListeners);
            }
        }

        private void NotifyEventListeners(IScreen screen, ScreenEvent screenEvent)
        {
            foreach (var eventListener in _eventListeners)
            {
                eventListener.OnScreenEvent(screen, screenEvent);
            }
        }
    }
}
