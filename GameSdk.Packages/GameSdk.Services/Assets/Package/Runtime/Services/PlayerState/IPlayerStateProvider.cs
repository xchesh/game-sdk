using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace GameSdk.Services.PlayerState
{
    public interface IPlayerStateProvider
    {
        PlayerStateProviderType Type { get; }
        bool IsEnabled { get; }

        UniTask Initialize();

        UniTask Save(IEnumerable<IPlayerState> states);
        UniTask Load(IEnumerable<IPlayerState> states);
        UniTask Preload(IEnumerable<IPlayerState> states);
        UniTask Delete(params string[] keys);

        void SetEnable(bool isEnable);
    }
}
