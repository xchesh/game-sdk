namespace Core.Common.UnityContainer
{
    public interface IInitializable
    {
        virtual int Order => 0;
        void Initialize();
    }
}
