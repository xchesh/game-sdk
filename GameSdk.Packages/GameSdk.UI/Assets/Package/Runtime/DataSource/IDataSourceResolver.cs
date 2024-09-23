public interface IDataSourceResolver
{
    T Resolve<T>();
    object Resolve(System.Type type);
}
