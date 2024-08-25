using UnityEngine;

namespace GameSdk.Services.InternetReachability
{
    [CreateAssetMenu(fileName = "GameSdk_InternetReachabilityConfig", menuName = "GameSds/Internet Reachability Config",
        order = 0)]
    public class InternetReachabilityConfig : ScriptableObject
    {
        [SerializeField] private string _checkURL = "https://google.com/generate_204";

        [SerializeField, Space(5)] private bool _checkOnAppFocus = true;
        [SerializeField] private bool _checkOnAppStart = true;

        [SerializeField, Space(5)] private bool _checkPeriodic = true;
        [SerializeField] private int _checkIntervalActiveMS = 60 * 1000;
        [SerializeField] private int _checkIntervalInactiveMS = 5 * 1000;

        public string CheckURL => _checkURL;
        public bool CheckOnAppFocus => _checkOnAppFocus;
        public bool CheckOnAppStart => _checkOnAppStart;
        public bool CheckPeriodic => _checkPeriodic;
        public int CheckIntervalActiveMS => _checkIntervalActiveMS;
        public int CheckIntervalInactiveMS => _checkIntervalInactiveMS;
    }
}
