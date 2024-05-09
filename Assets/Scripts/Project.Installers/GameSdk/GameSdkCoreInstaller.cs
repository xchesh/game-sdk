using Core.Common.UnityContainer;
using GameSdk.Core.Conditions;
using GameSdk.Core.Datetime;
using GameSdk.Core.Loggers;
using GameSdk.Core.Prices;
using GameSdk.Core.Rewards;
using GameSdk.Core.Rewards.Implement;
using UnityEngine;

namespace Project.Installers
{
    public class GameSdkCoreInstaller : IUnityInstaller
    {
        public override void InstallBindings(IUnityContainer container)
        {
            // Sdk - datetime
            container.Register<SystemTime>().As<ISystemTime>();
            // Sdk - logger
            container.Register<SystemLogger>().As<ISystemLogger>();
            container.RegisterInstance(Debug.unityLogger).As<ILogger>();
            // Sdk - Systems
            container.Register<ConditionsSystem>().As<IConditionsSystem>();
            container.Register<RewardsSystem>().As<IRewardsSystem>();
            container.Register<PricesSystem>().As<IPricesSystem>();
        }
    }
}