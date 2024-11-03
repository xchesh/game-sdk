using System;

namespace GameSdk.UnityContainer
{
    public interface IUnityContainerBuilder
    {
        IUnityContainerBuilderWithParameter As<TContract1>();
        IUnityContainerBuilderWithParameter As<TContract1, TContract2>();
        IUnityContainerBuilderWithParameter As<TContract1, TContract2, TContract3>();
        IUnityContainerBuilderWithParameter As(params Type[] contracts);

        IUnityContainerBuilderWithParameter AsSelf();
    }
}
