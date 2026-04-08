using System;
using UnityEngine;

namespace GameSdk.Services.NetworkConnectivity
{
    public interface INetworkConnectivityService
    {
        const string TAG = "NetworkConnectivity";

        event Action<bool> NetworkConnectionChanged;

        NetworkReachability NetworkReachability { get; }
        bool HasNetworkConnection { get; }

        void Initialize();

        Awaitable<bool> CheckNetworkConnectivity();
    }
}
