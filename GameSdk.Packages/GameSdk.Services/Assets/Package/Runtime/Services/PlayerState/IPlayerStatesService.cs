using Cysharp.Threading.Tasks;

namespace GameSdk.Services.PlayerState
{
    public interface IPlayerStatesService
    {
        UniTask Initialize();
        UniTask Load(PlayerStateProviderType type);
        UniTask Preload(PlayerStateProviderType type);
        UniTask Save();

        void SetProviderEnable(PlayerStateProviderType type, bool isEnable);
    }
}
