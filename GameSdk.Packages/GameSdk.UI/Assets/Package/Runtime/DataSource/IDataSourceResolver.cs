using System;
using Cysharp.Threading.Tasks;

public interface IDataSourceResolver : IDisposable
{
    bool IsInitialized { get; }

    UniTask Initialize();
    T Resolve<T>();
    object Resolve(System.Type type);
}
