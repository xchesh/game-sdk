# Changelog

## [1.1.0] - 2026-04-08

### Changed

- Breaking: replaced the public async API from `UniTask` with Unity `Awaitable`.
- Updated runtime async helpers to use Unity-native await points.

### Removed

- Removed the `com.cysharp.unitask` dependency from the package and project metadata.
