using System;
using VContainer;

namespace GameSdk.UnityContainer.VContainer
{
    public class VContainerRegistrationBuilder : IUnityContainerRegistrationBuilder
    {
        private RegistrationBuilder RegistrationBuilder { get; }

        public VContainerRegistrationBuilder(RegistrationBuilder registrationBuilder)
        {
            RegistrationBuilder = registrationBuilder;
        }

        public void As<TContract1>()
            => RegistrationBuilder.As<TContract1>();

        public void As<TContract1, TContract2>()
            => RegistrationBuilder.As<TContract1, TContract2>();

        public void As<TContract1, TContract2, TContract3>() =>
            RegistrationBuilder.As<TContract1, TContract2, TContract3>();

        public void As(params Type[] contracts)
            => RegistrationBuilder.As(contracts);

        public void AsSelf()
            => RegistrationBuilder.AsSelf();

        public IUnityContainerRegistrationBuilder WithParameter(Type type, object value)
        {
            RegistrationBuilder.WithParameter(type, value);

            return this;
        }

        public IUnityContainerRegistrationBuilder WithParameter<T>(T value)
        {
            RegistrationBuilder.WithParameter<T>(value);

            return this;
        }
    }
}
