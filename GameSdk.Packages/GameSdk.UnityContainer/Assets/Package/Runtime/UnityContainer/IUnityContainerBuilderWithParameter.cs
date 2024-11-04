using System;

namespace GameSdk.UnityContainer
{
    public interface IUnityContainerBuilderWithParameter : IUnityContainerBuilderNonLazy
    {
        IUnityContainerBuilderWithParameter WithParameter(Type type, object value);
        IUnityContainerBuilderWithParameter WithParameter<T>(T value);
    }
}
