using System;
using System.Collections.Generic;
using UnityEngine;


namespace Game.UI.Navigations
{
    [CreateAssetMenu(fileName = nameof(NavigationConfig), menuName = "Configs/Navigation/" + nameof(NavigationConfig))]
    public class NavigationConfig : ScriptableObject, INavigationConfig
    {
        [SerializeField] private List<ScreenConfig> _screenConfigs;

        public IList<IScreenConfig> Screens => new List<IScreenConfig>(_screenConfigs);
    }
}
