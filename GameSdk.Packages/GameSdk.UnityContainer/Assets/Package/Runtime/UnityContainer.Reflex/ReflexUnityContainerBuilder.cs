using System;
using Reflex.Core;
using UnityEngine;

namespace GameSdk.UnityContainer.Reflex
{
    public class ReflexUnityContainerBuilder<TConcrete> : IUnityContainerBuilder, IUnityContainerBuilderWithParameter
    {
        protected readonly ContainerBuilder _builder;
        protected readonly UnityContainerScope _scope;

        public ReflexUnityContainerBuilder(ContainerBuilder builder, UnityContainerScope scope)
        {
            _builder = builder;
            _scope = scope;
        }

        public IUnityContainerBuilderWithParameter As<TContract1>()
        {
            Add(typeof(TContract1));

            return this;
        }

        public IUnityContainerBuilderWithParameter As<TContract1, TContract2>()
        {
            Add(typeof(TContract1), typeof(TContract2));

            return this;
        }

        public IUnityContainerBuilderWithParameter As<TContract1, TContract2, TContract3>()
        {
            Add(typeof(TContract1), typeof(TContract2), typeof(TContract3));

            return this;
        }

        public IUnityContainerBuilderWithParameter As(params Type[] contracts)
        {
            Add(contracts);

            return this;
        }

        public IUnityContainerBuilderWithParameter AsSelf()
        {
            Add(typeof(TConcrete));

            return this;
        }

        public IUnityContainerBuilderWithParameter WithParameter(Type type, object value)
        {
            Debug.LogWarning("WithParameter is not supported by Reflex container");

            return this;
        }

        public IUnityContainerBuilderWithParameter WithParameter<T>(T value)
        {
            return WithParameter(typeof(T), value);
        }

        protected virtual void Add(params Type[] contracts)
        {
            var concrete = typeof(TConcrete);

            if (_scope == UnityContainerScope.Transient)
            {
                _builder.AddTransient(concrete, contracts);

                return;
            }

            _builder.AddSingleton(concrete, contracts);
        }
    }

    public class ReflexUnityContainerBuilderInstance<TConcrete> : ReflexUnityContainerBuilder<TConcrete>
    {
        private readonly TConcrete _instance;

        public ReflexUnityContainerBuilderInstance(TConcrete instance, ContainerBuilder builder, UnityContainerScope scope) : base(builder, scope)
        {
            _instance = instance;
        }

        protected override void Add(params Type[] contracts)
        {
            _builder.AddSingleton(_instance, contracts);
        }
    }
}
