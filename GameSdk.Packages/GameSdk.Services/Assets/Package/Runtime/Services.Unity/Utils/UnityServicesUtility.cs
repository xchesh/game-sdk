#if UNITY_SERVICES_CORE

using System;
using System.Threading.Tasks;
using GameSdk.Core.Loggers;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using UnityEngine;

namespace GameSdk.Services.Unity
{
    public static class UnityServicesUtility
    {
        public const string TAG = "UnityServices";

        private static Task<ServicesInitializationState> _initializeTask;

        public static async Awaitable<ServicesInitializationState> Initialize()
        {
            if (UnityServices.State == ServicesInitializationState.Initialized)
            {
                return UnityServices.State;
            }

            if (Utilities.CheckForInternetConnection() is false)
            {
                return ServicesInitializationState.Uninitialized;
            }

            if (_initializeTask == null || _initializeTask.IsCompleted)
            {
                _initializeTask = InitializeInternal();
            }

            return await _initializeTask;
        }

        private static async Task<ServicesInitializationState> InitializeInternal()
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
