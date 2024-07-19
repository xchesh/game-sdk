
using GameSdk.Core.Common;

namespace Game.UI.Navigations
{
    public interface INavigationActions
    {
        T Push<T>(string name, params IParameter[] parameters) where T : INavigationScreen;
        T PopTo<T>(string name, params IParameter[] parameters) where T : INavigationScreen;
        void Pop();
    }
}
