using System;

namespace Core.Common.UnityContainer
{
    public interface IUnityContainerRegistrationBuilder
    {
        void As<TContract1>();
        void As<TContract1, TContract2>();
        void As<TContract1, TContract2, TContract3>();
        void As(params Type[] contracts);

        void AsSelf();
    }
}
