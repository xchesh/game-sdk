using UnityEngine;

namespace GameSdk.Services.Authentication
{
    [CreateAssetMenu(fileName = "GameSds_Authentication", menuName = "GameSds/Authentication Config", order = 0)]
    public class AuthenticationConfig : ScriptableObject
    {
        [SerializeField] private bool _signInOnAppStart = true;

        public bool SignInOnAppStart => _signInOnAppStart;
    }
}