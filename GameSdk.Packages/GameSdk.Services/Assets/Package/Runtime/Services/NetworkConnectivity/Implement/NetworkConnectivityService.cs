using System;
using System.Net;
using Cysharp.Threading.Tasks;
using GameSdk.Core.Datetime;
using GameSdk.Core.Loggers;
using UnityEngine;
using UnityEngine.Networking;

namespace GameSdk.Services.NetworkConnectivity
{
    [JetBrains.Annotations.UsedImplicitly]
    public class NetworkConnectivityService : INetworkConnectivityService
    {
        private readonly ISystemTime _systemTime;
        private readonly ISystemLogger _systemLogger;
        private readonly NetworkConnectivityConfig _config;

        public event Action<bool> NetworkConnectionChanged;

        public NetworkReachability NetworkReachability { get; private set; }
        public bool HasNetworkConnection { get; private set; }

        private bool _isCheckActive;
        private long _lastCheckTimeMs;

        public NetworkConnectivityService(ISystemTime systemTime, ISystemLogger systemLogger, NetworkConnectivityConfig  config)
        {
            _systemTime = systemTime;
            _systemLogger = systemLogger;
            _config = config;

            _isCheckActive = true;
            _lastCheckTimeMs = 0;

            HasNetworkConnection = false;
            NetworkReachability = Application.internetReachability;
        }

        public void Initialize()
        {
            Application.focusChanged += FocusChangedHandler;
            Application.quitting += QuitHandler;

            if (_config.CheckOnAppStart)
            {
                _systemLogger.Log(LogType.Log, INetworkConnectivityService.TAG, "Initialize - check on app start");

                CheckNetworkConnectivity().Forget();
            }

            if (_config.CheckPeriodic)
            {
                _systemLogger.Log(LogType.Log, INetworkConnectivityService.TAG, "Initialize - start periodic check");

                RunFixedUpdate().Forget();
            }
        }

        public async UniTask<bool> CheckNetworkConnectivity()
        {
            bool hasInternetConnection;

            try
            {
                using var webRequest = UnityWebRequest.Get(_config.CheckURL);

                webRequest.timeout = _config.CheckTimeoutSec;

                await webRequest.SendWebRequest();

                hasInternetConnection = webRequest.result == UnityWebRequest.Result.Success;
            }
            catch
            {
                _systemLogger.Log(LogType.Log, INetworkConnectivityService.TAG, "No internet connection.");
                hasInternetConnection = false;
            }

            _lastCheckTimeMs = _systemTime.NowOffset.ToUnixTimeSeconds();

            if (HasNetworkConnection != hasInternetConnection)
            {
                HasNetworkConnection = hasInternetConnection;
                NetworkConnectionChanged?.Invoke(HasNetworkConnection);
            }

            return HasNetworkConnection;
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
                CheckNetworkConnectivity().Forget();
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
                _systemLogger.Log(LogType.Log, INetworkConnectivityService.TAG, "Network reachability changed.");
                NetworkReachability = Application.internetReachability;
                // Recheck connection if network reachability changed
                CheckNetworkConnectivity().Forget();
            }
        }

        private void UpdateCheckAuto()
        {
            var passedMs = _systemTime.NowOffset.ToUnixTimeSeconds() - _lastCheckTimeMs;
            var checkTimeMs = HasNetworkConnection ? _config.CheckIntervalActiveSec : _config.CheckIntervalInactiveSec;

            if (passedMs >= checkTimeMs)
            {
                CheckNetworkConnectivity().Forget();
            }
        }
    }
}
