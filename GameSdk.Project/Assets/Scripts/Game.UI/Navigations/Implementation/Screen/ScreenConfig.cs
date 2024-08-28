using System;
using GameSdk.Core.Essentials;
using GameSdk.Core.Toolbox;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.UI.Navigations
{

    [CreateAssetMenu(fileName = nameof(ScreenConfig), menuName = "Configs/Navigation/" + nameof(ScreenConfig))]
    public class ScreenConfig : ScriptableObject, IScreenConfig
    {
        [SerializeField, SerializedTypeDropdown(typeof(IScreen))]
        private SerializedType _type;

        [SerializeField] private VisualTreeAsset _asset;
        [SerializeField, Space(5)] private bool _isLazyLoad;

        public Type Type => _type.Value;
        public bool IsLazyLoad => _isLazyLoad;
        public VisualTreeAsset Asset => _asset;
    }
}
