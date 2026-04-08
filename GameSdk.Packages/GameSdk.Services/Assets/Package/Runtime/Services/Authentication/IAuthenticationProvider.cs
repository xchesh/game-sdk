using System;
using UnityEngine;

namespace GameSdk.Services.Authentication
{
    public interface IAuthenticationProvider
    {
        event Action SignedIn;
        event Action<Exception> SignInFailed;
        event Action SignedOut;
        event Action Expired;

        bool IsSignedIn { get; }

        Awaitable Initialize();
        Awaitable SignIn();
        void SignOut();
    }
}
