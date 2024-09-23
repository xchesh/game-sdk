using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameSdk.Core.Common;

namespace GameSdk.Sources.Navigation
{
    public static class NavigationsExtensions
    {
        public static T Navigate<T>(this INavigation navigation, T screen) where T : IScreen
        {
            return navigation.Push<T>(screen);
        }

        public static void GoBack(this INavigation navigation)
        {
            navigation.Pop();
        }
    }
}
