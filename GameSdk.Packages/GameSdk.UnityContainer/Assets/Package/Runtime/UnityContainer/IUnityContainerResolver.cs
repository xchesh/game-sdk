namespace GameSdk.UnityContainer
{
    public interface IUnityContainerResolver
    {
        T Resolve<T>();
        object Resolve(System.Type type);
    }
}
