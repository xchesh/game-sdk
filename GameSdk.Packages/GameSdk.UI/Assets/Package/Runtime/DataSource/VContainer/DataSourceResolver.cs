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

        IsInitialized = true;
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
