using System.Collections.Generic;
using UnityEngine;

namespace GameSdk.Services.PlayerState
{
    public interface IPlayerStateProvider
    {
        PlayerStateProviderType Type { get; }
        bool IsEnabled { get; }

        Awaitable Initialize();

        Awaitable Save(IEnumerable<IPlayerState> states);
        Awaitable Load(IEnumerable<IPlayerState> states);
        Awaitable Preload(IEnumerable<IPlayerState> states);
        Awaitable Delete(params string[] keys);

        void SetEnable(bool isEnable);
    }
}
