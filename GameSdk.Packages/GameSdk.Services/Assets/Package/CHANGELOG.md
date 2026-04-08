# Changelog

## [1.3.0] - 2026-04-08
- BREAKING: Migrated all public async contracts from `UniTask`/`UniTask<T>` to `Awaitable`/`Awaitable<T>`.
- Removed `com.cysharp.unitask` dependency from package metadata and assembly definitions.
- Replaced UniTask-specific internals (`WhenAll`, `Yield`, `ToAsyncLazy`, `UniTaskCompletionSource`) with Unity `Awaitable` and `Task` equivalents where needed.
