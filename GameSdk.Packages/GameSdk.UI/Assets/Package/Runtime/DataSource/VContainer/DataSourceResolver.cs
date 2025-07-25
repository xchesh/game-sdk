using System;
using Cysharp.Threading.Tasks;
using UnityEngine.Scripting;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

[UnityEngine.Scripting.Preserve]
public class DataSourceResolver : IDataSourceResolver
{
    private readonly LifetimeScope _scope;
    private readonly UIDocument _uiDocument;

    public bool IsInitialized { get; private set; }

    [RequiredMember]
    public DataSourceResolver(LifetimeScope scope, UIDocument uiDocument)
    {
        _scope = scope;
        _uiDocument = uiDocument;
    }

    public async UniTask Initialize()
    {
        await UniTask.WaitUntil(() => _uiDocument.runtimePanel != null);

        _uiDocument.runtimePanel.visualTree.dataSource = this;

        IsInitialized = _scope?.Container != null;

        if (IsInitialized is false)
        {
            UnityEngine.Debug.LogError("DataSourceResolver could not be initialized: VContainer 'Container' is null.");
        }
    }

    public T Resolve<T>()
    {
        // Resolve the type from the container
        return _scope.Container == null ? default : _scope.Container.Resolve<T>();
    }

    public object Resolve(Type type)
    {
        return _scope.Container?.ResolveNonGeneric(type);
    }

    public void Dispose()
    {
        if (_uiDocument.runtimePanel?.visualTree?.dataSource != null)
        {
            _uiDocument.runtimePanel.visualTree.dataSource = null;
        }

        IsInitialized = false;
    }
}
