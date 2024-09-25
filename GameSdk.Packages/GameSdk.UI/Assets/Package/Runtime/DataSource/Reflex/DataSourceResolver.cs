using Reflex.Core;
using UnityEngine.Scripting;

[UnityEngine.Scripting.Preserve]
public class DataSourceResolver : IDataSourceResolver
{
    private readonly Container _container;

    [RequiredMember]
    public DataSourceResolver(Container container)
    {
        _container = container;
    }

    public T Resolve<T>()
    {
        // Resolve the type from the container
        return _container.Single<T>();
    }
}
