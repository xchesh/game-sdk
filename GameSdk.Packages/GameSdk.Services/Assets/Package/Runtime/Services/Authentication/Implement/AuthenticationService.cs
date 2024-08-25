using System;
using Cysharp.Threading.Tasks;
using GameSdk.Core.Loggers;
using GameSdk.Services.InternetReachability;
using UnityEngine;

namespace GameSdk.Services.Authentication
{
    [JetBrains.Annotations.UsedImplicitly]
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ISystemLogger _systemLogger;
        private readonly AuthenticationConfig _config;
        private readonly IAuthenticationProvider _authenticationProvider;
        private readonly IInternetReachabilityService _internetReachabilityService;

        private UniTask _singInTask;

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
            IInternetReachabilityService internetReachabilityService)
        {
            _config = config;
            _systemLogger = systemLogger;
            _authenticationProvider = authenticationProvider;
            _internetReachabilityService = internetReachabilityService;
        }

        public async UniTask Initialize()
        {
            await _authenticationProvider.Initialize();

            if (_config.SignInOnAppStart)
            {
                await SignIn();
            }
        }

        public UniTask SignIn()
        {
            if (_singInTask is not { Status: UniTaskStatus.Pending })
            {
                _singInTask = SignInProcess().ToAsyncLazy().Task;
            }

            return _singInTask;
        }

        public void SignOut()
        {
            _authenticationProvider.SignOut();
        }

        private async UniTask SignInProcess()
        {
            if (_internetReachabilityService.IsInternetReachable is false)
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
