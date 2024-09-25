using System;
using Cysharp.Threading.Tasks;
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

        UniTask<bool> CheckNetworkConnectivity();
    }
}
