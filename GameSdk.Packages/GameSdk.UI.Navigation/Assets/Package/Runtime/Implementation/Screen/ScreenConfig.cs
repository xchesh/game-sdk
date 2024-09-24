using System;
using GameSdk.Core.Essentials;
using GameSdk.Core.Toolbox;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameSdk.UI.Navigation
{
    [CreateAssetMenu(fileName = nameof(ScreenConfig), menuName = "Configs/Navigation/" + nameof(ScreenConfig))]
    public class ScreenConfig : ScriptableObject, IScreenConfig
    {
        [SerializeField, SerializedTypeDropdown(typeof(IScreen))]
        private SerializedType _type;

        [SerializeField] private VisualTreeAsset _asset;
        [SerializeField] private bool _isLazyLoad;

        public Type Type => _type.Value;
        public bool IsLazyLoad => _isLazyLoad;
        public VisualTreeAsset Asset => _asset;
    }
}
