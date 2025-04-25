using Cysharp.Threading.Tasks;

public interface IDataSourceResolver
{
    bool IsInitialized { get; }

    UniTask Initialize();
    T Resolve<T>();
    object Resolve(System.Type type);
}
