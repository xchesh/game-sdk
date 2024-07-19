using System;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Simple fps counter.
/// </summary>
///
public class FpsCounter : MonoBehaviour
{
    public const int TARGET_FRAME_RATE = 60; // 60 for mobile platforms, -1 for fast as possible
    public const int BUFFER_SIZE = 50;  // Number of frames to sample

    [SerializeField] private UIDocument _document;

    private float _fpsValue;
    private int _currentIndex;
    private float[] _deltaTimeBuffer;

    private Label _fpsLabel;
    private bool _isEnabled;

    public float FpsValue => _fpsValue;

    // MonoBehaviour event messages
    private void Awake()
    {
        _isEnabled = true;
        _deltaTimeBuffer = new float[BUFFER_SIZE];

        Application.targetFrameRate = TARGET_FRAME_RATE;
    }

    private void OnEnable()
    {
        var root = _document.rootVisualElement;

        _fpsLabel = root.Q<Label>("fps-counter");

        if (_fpsLabel == null)
        {
            Debug.LogWarning("[FPSCounter]: Display label is null.");
            return;
        }
    }

    private void Update()
    {
        if (_isEnabled)
        {
            _deltaTimeBuffer[_currentIndex] = Time.deltaTime;
            _currentIndex = (_currentIndex + 1) % _deltaTimeBuffer.Length;
            _fpsValue = Mathf.RoundToInt(CalculateFps());

            _fpsLabel.text = $"FPS: {_fpsValue}";
        }

    }

    // Methods
    private float CalculateFps()
    {
        var totalTime = 0f;

        foreach (var deltaTime in _deltaTimeBuffer)
        {
            totalTime += deltaTime;
        }

        return _deltaTimeBuffer.Length / totalTime;
    }
}
