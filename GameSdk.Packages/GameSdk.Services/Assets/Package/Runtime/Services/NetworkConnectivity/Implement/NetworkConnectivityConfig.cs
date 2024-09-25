using UnityEngine;

namespace GameSdk.Services.NetworkConnectivity
{
    [CreateAssetMenu(fileName = "GameSdk_NetworkConnectivityConfig", menuName = "GameSds/Network Connectivity Config",
        order = 0)]
    public class NetworkConnectivityConfig : ScriptableObject
    {
        [SerializeField] private string _checkURL = "https://google.com/generate_204";

        [SerializeField, Space(5)] private bool _checkOnAppFocus = true;
        [SerializeField] private bool _checkOnAppStart = true;

        [SerializeField, Space(5)] private bool _checkPeriodic = true;
        [SerializeField] private int _checkTimeoutSec = 5;
        [SerializeField] private int _checkIntervalActiveSec = 30;
        [SerializeField] private int _checkIntervalInactiveSec = 5;

        public string CheckURL => _checkURL;
        public bool CheckOnAppFocus => _checkOnAppFocus;
        public bool CheckOnAppStart => _checkOnAppStart;
        public bool CheckPeriodic => _checkPeriodic;
        public int CheckTimeoutSec => _checkTimeoutSec;
        public int CheckIntervalActiveSec => _checkIntervalActiveSec;
        public int CheckIntervalInactiveSec => _checkIntervalInactiveSec;
    }
}
