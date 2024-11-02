using System;

namespace GameSdk.UnityContainer
{
    public interface IUnityContainerRegistrationBuilder
    {
        void As<TContract1>();
        void As<TContract1, TContract2>();
        void As<TContract1, TContract2, TContract3>();
        void As(params Type[] contracts);

        void AsSelf();

        IUnityContainerRegistrationBuilder WithParameter(string name, object value);
    }
}
