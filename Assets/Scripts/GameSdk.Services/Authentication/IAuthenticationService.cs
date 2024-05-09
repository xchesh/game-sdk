using Cysharp.Threading.Tasks;

namespace GameSdk.Services.Authentication
{
    public interface IAuthenticationService
    {
        UniTask Initialize();
    }
}