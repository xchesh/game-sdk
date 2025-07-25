using UnityEngine.Scripting;
using Zenject;

[UnityEngine.Scripting.Preserve]
public class DataSourceResolver : IDataSourceResolver
{
    private readonly DiContainer _diContainer;

    [RequiredMember]
    public DataSourceResolver(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    public T Resolve<T>()
    {
        // Resolve the type from the container
        return _diContainer.Resolve<T>();
    }

    public void Dispose()
    {
        _uiDocument.runtimePanel.visualTree.dataSource = null;

        IsInitialized = false;
    }
}
