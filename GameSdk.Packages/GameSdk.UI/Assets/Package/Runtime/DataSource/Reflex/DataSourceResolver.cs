using System;
using Cysharp.Threading.Tasks;
using Reflex.Core;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

[UnityEngine.Scripting.Preserve]
public class DataSourceResolver : IDataSourceResolver
{
    private readonly Container _container;
    private readonly UIDocument _uiDocument;

    [RequiredMember]
    public DataSourceResolver(Container container, UIDocument uiDocument)
    {
        _container = container;
        _uiDocument = uiDocument;
    }

    public async UniTask Initialize()
    {
        await UniTask.WaitUntil(() => _uiDocument.runtimePanel != null);

        _uiDocument.runtimePanel.visualTree.dataSource = this;
    }

    public T Resolve<T>()
    {
        return _container.Resolve<T>();
    }

    public object Resolve(Type type)
    {
        return _container.Resolve(type);
    }
}
