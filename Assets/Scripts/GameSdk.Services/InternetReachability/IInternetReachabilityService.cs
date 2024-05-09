using UnityEngine;

namespace GameSdk.Services.InternetReachability
{
    public interface IInternetReachabilityService
    {
        NetworkReachability NetworkReachability { get; }
        bool IsConnected { get; }
        
        void Initialize();

        bool CheckConnection();
    }
}