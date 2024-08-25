#if UNITY_SERVICES_CORE

using System;
using Cysharp.Threading.Tasks;
using GameSdk.Core.Loggers;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;

namespace GameSdk.Services.Unity
{
    public static class UnityServicesUtility
    {
        public const string TAG = "UnityServices";

        private static UniTask<ServicesInitializationState> _initializeTask;

        public static UniTask<ServicesInitializationState> Initialize()
        {
            if (UnityServices.State == ServicesInitializationState.Initialized)
            {
                return UniTask.FromResult(UnityServices.State);
            }

            if (Utilities.CheckForInternetConnection() is false)
            {
                return UniTask.FromResult(ServicesInitializationState.Uninitialized);
            }

            if (_initializeTask is not { Status: UniTaskStatus.Pending })
            {
                _initializeTask = InitializeInternal().ToAsyncLazy().Task;
            }

            return _initializeTask;
        }

        private static async UniTask<ServicesInitializationState> InitializeInternal()
        {
            SystemLog.Log(TAG, "Initializing Unity services...");

            try
            {
                await UnityServices.InitializeAsync();

                SystemLog.Log(TAG, "Unity services initialized successfully.");

                return UnityServices.State;
            }
            catch (Exception e)
            {
                SystemLog.Log(TAG, "Failed to initialize Unity services.");
                SystemLog.LogException(e);
            }

            return ServicesInitializationState.Uninitialized;
        }
    }
}

#endif
