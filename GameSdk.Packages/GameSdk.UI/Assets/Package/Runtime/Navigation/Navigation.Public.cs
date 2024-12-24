using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameSdk.UI
{
    public partial class Navigation
    {
        public Screen CurrentScreen => HistoryLast?.Element;
        public IEnumerable<Screen> Screens => _screens.Values;

        public event Action<Screen> ScreenChanged;

        public T GetScreen<T>() where T : Screen
        {
            return (T)GetScreen(typeof(T));
        }

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

        public Screen ReplaceTo(Screen oldScreen, Screen newScreen, object newScreenData = null)
        {
            while (CurrentScreen != oldScreen && _history.Count > 1)
            {
                // Hide the current screen
                HideLastScreen();
                // Release the current history item
                ReleaseHistoryItem(_history.Pop());
            }

            return Replace(newScreen, newScreenData);
        }

        public Screen Push(Screen screen, object data = null)
        {
            // If history is not empty, blur the last screen
            BlurLastScreen();
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

        public void Cancel()
        {
            if (CurrentScreen?.OnCancel() is false)
            {
                return;
            }

            Pop();
        }

        public VisualElement GetScreenElement<T>() where T : Screen
        {
            return GetScreenElement(GetScreen<T>());
        }

        private void BlurLastScreen()
        {
            if (_history.Count < 1)
            {
                return;
            }

            var last = HistoryLast;

            if (last?.Element == null)
            {
                return;
            }

            // Blur the last screen
            BlurScreen(last.Element);
        }

        private void ShowLastScreen()
        {
            if (_history.Count < 1)
            {
                return;
            }

            var last = HistoryLast;

            if (last?.Element == null)
            {
                return;
            }

            // Set the data of the last screen
            last.Element.SetData(last.Data);
            // Show the last screen
            ShowScreen(last.Element);
        }

        private void HideLastScreen()
        {
            if (_history.Count < 1)
            {
                return;
            }

            var last = HistoryLast;

            if (last?.Element == null)
            {
                return;
            }

            // Reset the data of the last screen
            last.Element.SetData(null);
            // Hide the last screen
            HideScreen(last.Element);
        }
    }
}
