using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameSdk.Sources.Core.Common;

namespace Game.UI.Navigations
{
    public static class NavigationsExtensions
    {
        public static T Navigate<T>(this INavigation navigation, string name, T screen, params IParameter[] parameters) where T : INavigationScreen
        {
            return navigation.Push<T>(name, parameters);
        }

        public static void GoBack(this INavigation navigation)
        {
            navigation.Pop();
        }
    }
}
