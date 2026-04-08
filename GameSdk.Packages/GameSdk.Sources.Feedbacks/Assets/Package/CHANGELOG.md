# Changelog

## [1.1.0] - 2026-04-08

### Changed

- Breaking: replaced the public async API from `UniTask` with Unity `Awaitable`.
- Migrated feedback playback and strategy implementations to Unity-native await points.

### Removed

- Removed the `com.cysharp.unitask` dependency from package metadata and project manifest references.
