using UnityEngine;

namespace GameSdk.Services.PlayerState
{
    public interface IPlayerStatesService
    {
        Awaitable Initialize();
        Awaitable Load(PlayerStateProviderType type);
        Awaitable Preload(PlayerStateProviderType type);
        Awaitable Save();

        void SetProviderEnable(PlayerStateProviderType type, bool isEnable);
    }
}
