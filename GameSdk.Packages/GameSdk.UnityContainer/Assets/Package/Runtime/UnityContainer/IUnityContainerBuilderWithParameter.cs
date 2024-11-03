using System;

namespace GameSdk.UnityContainer
{
    public interface IUnityContainerBuilderWithParameter
    {
        IUnityContainerBuilderWithParameter WithParameter(Type type, object value);
        IUnityContainerBuilderWithParameter WithParameter<T>(T value);
    }
}
