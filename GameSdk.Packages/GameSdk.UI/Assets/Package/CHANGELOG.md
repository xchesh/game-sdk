# Changelog

## [1.4.0] - 2026-04-08
- BREAKING: Migrated public async contract from `UniTask` to Unity `Awaitable`.
- Replaced `UniTask.WaitUntil` usage with `Awaitable`-based frame waiting in data source resolvers.
- Removed `com.cysharp.unitask` from package and project metadata, and dropped `UniTask` assembly references.
