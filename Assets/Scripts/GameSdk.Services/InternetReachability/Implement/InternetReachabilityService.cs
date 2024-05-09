using System.Net;
using Cysharp.Threading.Tasks;
using GameSdk.Core.Datetime;
using UnityEngine;

namespace GameSdk.Services.InternetReachability
{
    public class InternetReachabilityService : IInternetReachabilityService
    {
        private readonly ISystemTime _systemTime;
        private readonly InternetReachabilityConfig _config;

        public NetworkReachability NetworkReachability { get; private set; }
        public bool IsConnected { get; private set; }

        private bool _isActive;
        private long _lastCheckTimeMs;

        public InternetReachabilityService(ISystemTime systemTime, InternetReachabilityConfig config)
        {
            _systemTime = systemTime;
            _config = config;
            _isActive = true;

            IsConnected = false;
            NetworkReachability = Application.internetReachability;
        }

        public void Initialize()
        {
            Application.focusChanged += FocusChangedHandler;
            Application.quitting += QuitHandler;

            if (_config.CheckOnAppStart)
            {
                CheckConnection();
            }

            if (_config.CheckPeriodic)
            {
                RunFixedUpdate().Forget();
            }
        }

        public bool CheckConnection()
        {
            try
            {
                using var client = new WebClient();
                using (client.OpenRead(_config.CheckURL))
                {
                    IsConnected = true;
                }
            }
            catch
            {
                IsConnected = false;
            }

            _lastCheckTimeMs = _systemTime.OffsetNow().ToUnixTimeMilliseconds();

            return IsConnected;
        }

        private void QuitHandler()
        {
            _isActive = false;
        }

        private void FocusChangedHandler(bool hasFocus)
        {
            _isActive = hasFocus;

            if (_isActive && _config.CheckOnAppFocus)
            {
                CheckConnection();
            }
        }

        private async UniTaskVoid RunFixedUpdate()
        {
            while (_isActive)
            {
                UpdateUnityCheck();
                UpdateAutoCheck();

                await UniTask.Yield(PlayerLoopTiming.LastFixedUpdate);
            }
        }

        private void UpdateUnityCheck()
        {
            if (NetworkReachability != Application.internetReachability)
            {
                NetworkReachability = Application.internetReachability;
                // Recheck connection if network reachability changed
                CheckConnection();
            }
        }

        private void UpdateAutoCheck()
        {
            var passedMs = _systemTime.OffsetNow().ToUnixTimeMilliseconds() - _lastCheckTimeMs;
            var checkTimeMs = IsConnected ? _config.CheckIntervalActiveMS : _config.CheckIntervalInactiveMS;

            if (passedMs >= checkTimeMs)
            {
                CheckConnection();
            }
        }
    }
}