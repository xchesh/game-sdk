using UnityEngine;

namespace GameSdk.Sources.Services.Authentication
{
    [CreateAssetMenu(fileName = "GameSdk_AuthenticationConfig", menuName = "GameSds/Authentication Config", order = 0)]
    public class AuthenticationConfig : ScriptableObject
    {
        [SerializeField] private bool _signInOnAppStart = true;

        public bool SignInOnAppStart => _signInOnAppStart;
    }
}
