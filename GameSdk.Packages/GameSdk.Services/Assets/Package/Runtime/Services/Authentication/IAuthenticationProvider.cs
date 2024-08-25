using System;
using Cysharp.Threading.Tasks;

namespace GameSdk.Services.Authentication
{
    public interface IAuthenticationProvider
    {
        event Action SignedIn;
        event Action<Exception> SignInFailed;
        event Action SignedOut;
        event Action Expired;

        bool IsSignedIn { get; }

        UniTask Initialize();
        UniTask SignIn();
        void SignOut();
    }
}
