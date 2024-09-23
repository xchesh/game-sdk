using System.Collections.Generic;

namespace GameSdk.Sources.Navigation
{
    public interface INavigationConfig
    {
        IList<IScreenConfig> Screens { get; }

        bool HasScreenConfig<T>() where T : IScreen
        {
            return GetScreenConfig<T>() != null;
        }

        IScreenConfig GetScreenConfig(System.Type type)
        {
            foreach (var screenConfig in Screens)
            {
                if (screenConfig.Type == type)
                {
                    return screenConfig;
                }
            }

            return null;
        }

        IScreenConfig GetScreenConfig<T>() where T : IScreen
        {
            return GetScreenConfig(typeof(T));
        }
    }
}
