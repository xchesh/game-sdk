using System;
using System.Net;
using Cysharp.Threading.Tasks;
using GameSdk.Core.Datetime;
using GameSdk.Core.Loggers;
using UnityEngine;

namespace GameSdk.Services.InternetReachability
{
    [JetBrains.Annotations.UsedImplicitly]
    public class InternetReachabilityService : IInternetReachabilityService
    {
        private readonly ISystemTime _systemTime;
        private readonly ISystemLogger _systemLogger;
        private readonly InternetReachabilityConfig _config;

        public event Action<bool> InternetReachable;

        public NetworkReachability NetworkReachability { get; private set; }
        public bool IsInternetReachable { get; private set; }

        private bool _isCheckActive;
        private long _lastCheckTimeMs;

        public InternetReachabilityService(
            ISystemTime systemTime,
            ISystemLogger systemLogger,
            InternetReachabilityConfig config)
        {
            _systemTime = systemTime;
            _systemLogger = systemLogger;
            _config = config;

            _isCheckActive = true;
            _lastCheckTimeMs = 0;

            IsInternetReachable = false;
            NetworkReachability = Application.internetReachability;
        }

        public void Initialize()
        {
            Application.focusChanged += FocusChangedHandler;
            Application.quitting += QuitHandler;

            if (_config.CheckOnAppStart)
            {
                _systemLogger.Log(LogType.Log, IInternetReachabilityService.TAG, "Initialize - check on app start");
                CheckInternetReachability();
            }

            if (_config.CheckPeriodic)
            {
                _systemLogger.Log(LogType.Log, IInternetReachabilityService.TAG, "Initialize - start periodic check");
                RunFixedUpdate().Forget();
            }
        }

        public bool CheckInternetReachability()
        {
            bool isInternetReachable;

            try
            {
                using var client = new WebClient();
                using (client.OpenRead(_config.CheckURL))
                {
                    isInternetReachable = true;
                }
            }
            catch
            {
                _systemLogger.Log(LogType.Log, IInternetReachabilityService.TAG, "No internet connection.");
                isInternetReachable = false;
            }

            _lastCheckTimeMs = _systemTime.OffsetNow().ToUnixTimeMilliseconds();

            if (IsInternetReachable != isInternetReachable)
            {
                IsInternetReachable = isInternetReachable;
                InternetReachable?.Invoke(IsInternetReachable);
            }

            return IsInternetReachable;
        }

        private void QuitHandler()
        {
            _isCheckActive = false;
        }

        private void FocusChangedHandler(bool hasFocus)
        {
            _isCheckActive = hasFocus;

            if (_isCheckActive && _config.CheckOnAppFocus)
            {
                CheckInternetReachability();
            }
        }

        private async UniTaskVoid RunFixedUpdate()
        {
            while (_isCheckActive)
            {
                UpdateCheckUnity();
                UpdateCheckAuto();

                await UniTask.Yield(PlayerLoopTiming.FixedUpdate);
            }
        }

        private void UpdateCheckUnity()
        {
            if (NetworkReachability != Application.internetReachability)
            {
                _systemLogger.Log(LogType.Log, IInternetReachabilityService.TAG, "Network reachability changed.");
                NetworkReachability = Application.internetReachability;
                // Recheck connection if network reachability changed
                CheckInternetReachability();
            }
        }

        private void UpdateCheckAuto()
        {
            var passedMs = _systemTime.OffsetNow().ToUnixTimeMilliseconds() - _lastCheckTimeMs;
            var checkTimeMs = IsInternetReachable ? _config.CheckIntervalActiveMS : _config.CheckIntervalInactiveMS;

            if (passedMs >= checkTimeMs)
            {
                CheckInternetReachability();
            }
        }
    }
}