using System;
using UnityEngine;

namespace GameSdk.Sources.Services.InternetReachability
{
    public interface IInternetReachabilityService
    {
        const string TAG = "InternetReachability";

        event Action<bool> InternetReachable;

        NetworkReachability NetworkReachability { get; }
        bool IsInternetReachable { get; }

        void Initialize();

        bool CheckInternetReachability();
    }
}
