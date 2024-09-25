using System;
using VContainer;
using VContainer.Unity;
using UnityEngine.Scripting;

[UnityEngine.Scripting.Preserve]
public class DataSourceResolver : IDataSourceResolver
{
    private readonly LifetimeScope _scope;

    [RequiredMember]
    public DataSourceResolver(LifetimeScope scope)
    {
        _scope = scope;
    }

    public T Resolve<T>()
    {
        // Resolve the type from the container
        return _scope.Container.Resolve<T>();
    }

    public object Resolve(Type type)
    {
        UnityEngine.Debug.Log("Resolve non generic");

        return _scope.Container.ResolveNonGeneric(type);
    }
}
