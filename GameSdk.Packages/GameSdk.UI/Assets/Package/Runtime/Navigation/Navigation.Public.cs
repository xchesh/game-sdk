using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameSdk.UI
{
    public partial class Navigation
    {
        public Screen CurrentScreen => HistoryLast?.Element;
        public IEnumerable<Screen> Screens => _screens.Values;

        public event Action<Screen> ScreenChanged;

        public Screen GetScreen(Type screenType)
        {
            if (_screens.TryGetValue(screenType, out var screen) is false)
            {
                // If the screen is not found, throw an exception
                throw new KeyNotFoundException($"Screen of type {screenType} not found.");
            }

            return screen;
        }

        public Screen Replace(Screen screen, object data = null)
        {
            // Hide the current screen
            HideLastScreen();
            // Release the current history item
            ReleaseHistoryItem(_history.Pop());
            // Push the new screen to the history
            return Push(screen, data);
        }

        public Screen Push(Screen screen, object data = null)
        {
            // Push the new screen to the history
            _history.Push(CreateHistoryItem(screen, data));
            // Show the new screen
            ShowLastScreen();

            ScreenChanged?.Invoke(screen);

            return screen;
        }

        public Screen Pop()
        {
            // If there is no history or only one screen in the history, return
            if (HistoryLast == null || _history.Count <= 1)
            {
                return CurrentScreen;
            }
            // Hide the current screen
            HideLastScreen();
            // Release the current history item
            ReleaseHistoryItem(_history.Pop());
            // Show the previous screen
            ShowLastScreen();

            ScreenChanged?.Invoke(CurrentScreen);

            return CurrentScreen;
        }

        private void ShowLastScreen()
        {
            var last = HistoryLast;
            // Set the data of the last screen
            last.Element.SetData(last.Data);
            // Show the last screen
            ShowScreen(last.Element);
        }

        private void HideLastScreen()
        {
            var last = HistoryLast;
            // Reset the data of the last screen
            last.Element.SetData(null);
            // Hide the last screen
            HideScreen(last.Element);
        }
    }
}
