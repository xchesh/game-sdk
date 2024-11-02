using System.ComponentModel;

namespace GameSdk.Services.PlayerState
{
    public interface IPlayerState : INotifyPropertyChanged
    {
        string Key { get; }

        string ToJson();
        void FromJson(string json);
    }
}
