using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Navigation
{
    [Serializable]
    public struct ScreenConfig
    {
        [SerializeField] private string _name;
        [SerializeField] private VisualTreeAsset _screen;
        [SerializeField, Space(5)] private ScreenConfig[] _children;

        public string Name => _name;
        public ScreenConfig[] Children => _children;
        public VisualTreeAsset Screen => _screen;
    }
}
