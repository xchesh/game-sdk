using System;
using UnityEngine;

namespace Navigation
{
    [CreateAssetMenu(fileName = "NavigationConfig", menuName = "Project/NavigationConfig", order = 0)]
    public class NavigationConfig : ScriptableObject
    {
        [SerializeField] private ScreenConfig[] _screens;

        public ScreenConfig[] Screens => _screens;
    }
}
