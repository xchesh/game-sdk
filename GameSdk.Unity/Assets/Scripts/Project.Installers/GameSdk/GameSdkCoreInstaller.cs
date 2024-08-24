using GameSdk.Sources.Core.Conditions;
using GameSdk.Sources.Core.Datetime;
using GameSdk.Sources.Core.Loggers;
using GameSdk.Sources.Core.Prices;
using GameSdk.Sources.Core.Rewards;
using Project.Common.UnityContainer;
using UnityEngine;

namespace Project.Installers
{
    public class GameSdkCoreInstaller : IUnityInstaller
    {
        public override void InstallBindings(IUnityContainer container)
        {
            // Sdk - SystemTime
            container.Register<SystemTime>().As<ISystemTime>();
            // Sdk - Logger
            container.Register<SystemLogger>().As<ISystemLogger>();
            container.RegisterInstance(Debug.unityLogger).As<ILogger>();
            // Sdk - Conditions
            container.Register<ConditionsSystem>().As<IConditionsSystem>();
            container.Register<ConditionsContext>().As<IConditionsContext>();
            // Sdk - Rewards
            container.Register<RewardsSystem>().As<IRewardsSystem>();
            container.Register<RewardsContext>().As<IRewardsContext>();
            // Sdk - Prices
            container.Register<PricesSystem>().As<IPricesSystem>();
            container.Register<PricesContext>().As<IPricesContext>();
        }
    }
}
