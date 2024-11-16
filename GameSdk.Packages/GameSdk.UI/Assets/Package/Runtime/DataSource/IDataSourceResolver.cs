using Cysharp.Threading.Tasks;

public interface IDataSourceResolver
{
    UniTask Initialize();
    T Resolve<T>();
    object Resolve(System.Type type);
}
