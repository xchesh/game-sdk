#if UNITY_SERVICES_CORE && UNITY_SERVICES_AUTHENTICATION

using System;
using Cysharp.Threading.Tasks;
using GameSdk.Core.Loggers;
using GameSdk.Services.Authentication;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using AuthenticationService = Unity.Services.Authentication.AuthenticationService;
using IAuthenticationService = Unity.Services.Authentication.IAuthenticationService;

namespace GameSdk.Services.Unity
{
    public class UnityAuthenticationProvider : IAuthenticationProvider
    {
        private const string TAG = "UnityAuthentication";
        private static IAuthenticationService UnityAuth => AuthenticationService.Instance;

        private readonly ISystemLogger _systemLogger;

        public event Action SignedIn
        {
            add => UnityAuth.SignedIn += value;
            remove => UnityAuth.SignedIn -= value;
        }

        public event Action<Exception> SignInFailed
        {
            add => UnityAuth.SignInFailed += value;
            remove => UnityAuth.SignInFailed -= value;
        }

        public event Action SignedOut
        {
            add => UnityAuth.SignedOut += value;
            remove => UnityAuth.SignedOut -= value;
        }

        public event Action Expired
        {
            add => UnityAuth.Expired += value;
            remove => UnityAuth.Expired -= value;
        }

        public bool IsSignedIn => UnityAuth.IsSignedIn;

        public UnityAuthenticationProvider(ISystemLogger systemLogger)
        {
            _systemLogger = systemLogger;
        }

        public UniTask Initialize()
        {
            return UnityServicesUtility.Initialize();
        }

        public async UniTask SignIn()
        {
            try
            {
                if (await UnityServicesUtility.Initialize() != ServicesInitializationState.Initialized)
                {
                    throw new RequestFailedException(
                        CommonErrorCodes.ServiceUnavailable,
                        "Unity Services not initialized"
                    );
                }

                // TODO: Implement different sign-in methods later
                await AuthenticationService.Instance.SignInAnonymouslyAsync();

                _systemLogger.Log(LogType.Log, TAG, "Sign in anonymously succeeded!");
                _systemLogger.Log(LogType.Log, TAG, $"PlayerId - {AuthenticationService.Instance.PlayerId}");
            }
            catch (AuthenticationException ex)
            {
                // Compare error code to AuthenticationErrorCodes
                // Notify the player with the proper error message
                _systemLogger.LogException(ex);
            }
            catch (RequestFailedException ex)
            {
                // Compare error code to CommonErrorCodes
                // Notify the player with the proper error message
                _systemLogger.LogException(ex);
            }
        }

        public void SignOut()
        {
            try
            {
                if (AuthenticationService.Instance.IsSignedIn)
                {
                    AuthenticationService.Instance.SignOut();
                }

                _systemLogger.Log(LogType.Log, TAG, "Sign out succeeded!");
            }
            catch (AuthenticationException ex)
            {
                // Compare error code to AuthenticationErrorCodes
                // Notify the player with the proper error message
                _systemLogger.LogException(ex);
            }
            catch (RequestFailedException ex)
            {
                // Compare error code to CommonErrorCodes
                // Notify the player with the proper error message
                _systemLogger.LogException(ex);
            }
        }
    }
}

#endif
