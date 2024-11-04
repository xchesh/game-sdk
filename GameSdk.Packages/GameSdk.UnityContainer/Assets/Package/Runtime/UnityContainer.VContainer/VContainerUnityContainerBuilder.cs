using System;
using System.Collections.Generic;
using VContainer;

namespace GameSdk.UnityContainer.VContainer
{
    public class VContainerUnityContainerBuilder : IUnityContainerBuilder, IUnityContainerBuilderWithParameter
    {
        private Type ImplementationType { get; }
        private List<Type> RegisteredTypes { get; }
        private VContainerUnityContainer UnityContainer { get; }
        private RegistrationBuilder RegistrationBuilder { get; }

        public VContainerUnityContainerBuilder(VContainerUnityContainer unityContainer, Type type, RegistrationBuilder registrationBuilder)
        {
            RegisteredTypes = new List<Type>();
            ImplementationType = type;

            UnityContainer = unityContainer;
            RegistrationBuilder = registrationBuilder;
        }

        public IUnityContainerBuilderWithParameter As<TContract1>()
        {
            RegistrationBuilder.As<TContract1>();
            RegisteredTypes.Add(typeof(TContract1));

            return this;
        }

        public IUnityContainerBuilderWithParameter As<TContract1, TContract2>()
        {
            RegistrationBuilder.As<TContract1, TContract2>();
            RegisteredTypes.Add(typeof(TContract1));
            RegisteredTypes.Add(typeof(TContract2));

            return this;
        }

        public IUnityContainerBuilderWithParameter As<TContract1, TContract2, TContract3>()
        {
            RegistrationBuilder.As<TContract1, TContract2, TContract3>();
            RegisteredTypes.Add(typeof(TContract1));
            RegisteredTypes.Add(typeof(TContract2));
            RegisteredTypes.Add(typeof(TContract3));

            return this;
        }

        public IUnityContainerBuilderWithParameter As(params Type[] contracts)
        {
            RegistrationBuilder.As(contracts);
            RegisteredTypes.AddRange(contracts);

            return this;
        }

        public IUnityContainerBuilderWithParameter AsSelf()
        {
            RegistrationBuilder.AsSelf();
            RegisteredTypes.Add(ImplementationType);

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

        public void NonLazy()
        {
            UnityContainer.NonLazyTypes.AddRange(RegisteredTypes);
        }
    }
}
