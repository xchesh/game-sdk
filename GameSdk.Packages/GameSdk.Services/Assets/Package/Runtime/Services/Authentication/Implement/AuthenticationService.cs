using System;
using System.Threading.Tasks;
using GameSdk.Core.Loggers;
using GameSdk.Services.NetworkConnectivity;
using UnityEngine;

namespace GameSdk.Services.Authentication
{
    [JetBrains.Annotations.UsedImplicitly]
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ISystemLogger _systemLogger;
        private readonly AuthenticationConfig _config;
        private readonly IAuthenticationProvider _authenticationProvider;
        private readonly INetworkConnectivityService _internetReachabilityService;

        private Task _signInTask;

        public event Action SignedIn
        {
            add => _authenticationProvider.SignedIn += value;
            remove => _authenticationProvider.SignedIn -= value;
        }

        public event Action<Exception> SignInFailed
        {
            add => _authenticationProvider.SignInFailed += value;
            remove => _authenticationProvider.SignInFailed -= value;
        }

        public event Action SignedOut
        {
            add => _authenticationProvider.SignedOut += value;
            remove => _authenticationProvider.SignedOut -= value;
        }

        public event Action Expired
        {
            add => _authenticationProvider.Expired += value;
            remove => _authenticationProvider.Expired -= value;
        }

        public bool IsSignedIn => _authenticationProvider.IsSignedIn;

        public AuthenticationService(
            ISystemLogger systemLogger,
            AuthenticationConfig config,
            IAuthenticationProvider authenticationProvider,
            INetworkConnectivityService internetReachabilityService)
        {
            _config = config;
            _systemLogger = systemLogger;
            _authenticationProvider = authenticationProvider;
            _internetReachabilityService = internetReachabilityService;
        }

        public async Awaitable Initialize()
        {
            await _authenticationProvider.Initialize();

            if (_config.SignInOnAppStart)
            {
                await SignIn();
            }
        }

        public async Awaitable SignIn()
        {
            if (_signInTask == null || _signInTask.IsCompleted)
            {
                _signInTask = SignInProcess();
            }

            await _signInTask;
        }

        public void SignOut()
        {
            _authenticationProvider.SignOut();
        }

        private async Task SignInProcess()
        {
            if (_internetReachabilityService.HasNetworkConnection is false)
            {
                _systemLogger.Log(LogType.Error, IAuthenticationService.TAG, "SignIn failed. No internet connection.");

                return;
            }

            if (_authenticationProvider.IsSignedIn)
            {
                _systemLogger.Log(LogType.Log, IAuthenticationService.TAG, "SignIn Success. User already sign-in.");
            }

            await _authenticationProvider.SignIn();
        }
    }
}
