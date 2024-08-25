
using System;
using Reflex.Core;

namespace GameSdk.UnityContainer.Reflex
{
    public class ReflexRegistrationBuilder<TConcrete> : IUnityContainerRegistrationBuilder
    {
        protected readonly ContainerBuilder _builder;
        protected readonly UnityContainerScope _scope;

        public ReflexRegistrationBuilder(ContainerBuilder builder, UnityContainerScope scope)
        {
            _builder = builder;
            _scope = scope;
        }

        public void As<TContract1>()
        {
            Add(typeof(TContract1));
        }

        public void As<TContract1, TContract2>()
        {
            Add(typeof(TContract1), typeof(TContract2));
        }

        public void As<TContract1, TContract2, TContract3>()
        {
            Add(typeof(TContract1), typeof(TContract2), typeof(TContract3));
        }

        public void As(params Type[] contracts)
        {
            Add(contracts);
        }

        public void AsSelf()
        {
            Add(typeof(TConcrete));
        }

        protected virtual void Add(params Type[] contracts)
        {
            Type concrete = typeof(TConcrete);

            if (_scope == UnityContainerScope.Transient)
            {
                _builder.AddTransient(concrete, contracts);

                return;
            }

            _builder.AddSingleton(concrete, contracts);
        }
    }

    public class ReflexRegistrationBuilderInstance<TConcrete> : ReflexRegistrationBuilder<TConcrete>
    {
        private readonly TConcrete _instance;

        public ReflexRegistrationBuilderInstance(TConcrete instance, ContainerBuilder builder, UnityContainerScope scope) : base(builder, scope)
        {
            _instance = instance;
        }

        protected override void Add(params Type[] contracts)
        {
            if (_scope == UnityContainerScope.Transient)
            {
                _builder.AddTransient(_instance, contracts);

                return;
            }

            _builder.AddSingleton(_instance, contracts);
        }
    }
}
