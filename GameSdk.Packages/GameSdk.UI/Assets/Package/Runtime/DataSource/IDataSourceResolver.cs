using System;
using UnityEngine;

public interface IDataSourceResolver : IDisposable
{
    bool IsInitialized { get; }

    Awaitable Initialize();
    T Resolve<T>();
    object Resolve(System.Type type);
}
