using System;
using VContainer;

namespace GameSdk.UnityContainer.VContainer
{
    public class VContainerUnityContainerBuilder : IUnityContainerBuilder, IUnityContainerBuilderWithParameter
    {
        private RegistrationBuilder RegistrationBuilder { get; }

        public VContainerUnityContainerBuilder(RegistrationBuilder registrationBuilder)
        {
            RegistrationBuilder = registrationBuilder;
        }

        public IUnityContainerBuilderWithParameter As<TContract1>()
        {
            RegistrationBuilder.As<TContract1>();

            return this;
        }

        public IUnityContainerBuilderWithParameter As<TContract1, TContract2>()
        {
            RegistrationBuilder.As<TContract1, TContract2>();

            return this;
        }

        public IUnityContainerBuilderWithParameter As<TContract1, TContract2, TContract3>()
        {
            RegistrationBuilder.As<TContract1, TContract2, TContract3>();

            return this;
        }

        public IUnityContainerBuilderWithParameter As(params Type[] contracts)
        {
            RegistrationBuilder.As(contracts);

            return this;
        }

        public IUnityContainerBuilderWithParameter AsSelf()
        {
            RegistrationBuilder.AsSelf();

            return this;
        }

        public IUnityContainerBuilderWithParameter WithParameter(Type type, object value)
        {
            RegistrationBuilder.WithParameter(type, value);

            return this;
        }

        public IUnityContainerBuilderWithParameter WithParameter<T>(T value)
        {
            RegistrationBuilder.WithParameter<T>(value);

            return this;
        }
    }
}
