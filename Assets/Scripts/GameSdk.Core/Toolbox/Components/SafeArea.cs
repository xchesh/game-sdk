using UnityEngine;

namespace GameSdk.Core.Toolbox
{
    public class SafeArea : MonoBehaviour
    {
        [SerializeField] private RectTransform _panel;

        [SerializeField, Space(5)] private Vector2 _anchorMin = new Vector2(0, 0);
        [SerializeField] private Vector2 _anchorMax = new Vector2(1, 1);

        private Rect _lastSafeArea = new Rect(0, 0, 0, 0);

        private void OnEnable()
        {
            ApplySafeArea(Screen.safeArea);
        }

        private void Update()
        {
            if (_panel == null)
            {
                return;
            }

            UpdateSafeArea();
        }

        private void UpdateSafeArea()
        {
            var safeArea = Screen.safeArea;
#if UNITY_EDITOR
            if (Screen.width == 1125 && Screen.height == 2436)
            {
                safeArea.y = 102;
                safeArea.height = 2202;
            }

            if (Screen.width == 2436 && Screen.height == 1125)
            {
                safeArea.x = 132;
                safeArea.y = 63;
                safeArea.height = 1062;
                safeArea.width = 2172;
            }
#endif
            if (safeArea != _lastSafeArea)
            {
                ApplySafeArea(safeArea);
            }
        }

        private void ApplySafeArea(Rect area)
        {
            _panel.anchoredPosition = Vector2.zero;
            _panel.sizeDelta = Vector2.zero;

            var anchorMin = area.position;
            var anchorMax = area.position + area.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            _panel.anchorMin = new Vector2(Mathf.Max(anchorMin.x, _anchorMin.x, 0),
                Mathf.Max(anchorMin.y, _anchorMin.y, 0));
            _panel.anchorMax = new Vector2(Mathf.Min(anchorMax.x, _anchorMax.x, 1),
                Mathf.Min(anchorMax.y, _anchorMax.y, 1));

            _lastSafeArea = area;
        }

        private void OnValidate()
        {
            if (_panel == null)
            {
                _panel = GetComponent<RectTransform>();
            }

            _lastSafeArea = new Rect(0, 0, 0, 0);

            UpdateSafeArea();
        }
    }
}