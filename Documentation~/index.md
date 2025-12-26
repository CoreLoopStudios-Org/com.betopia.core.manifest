# Coreloop AssetManifest

`AssetManifest<T>` is a generic `ScriptableObject` that stores a list of `(asset, guid)` pairs and builds lookup dictionaries at runtime.

## Editor workflow

Because runtime assemblies cannot reference `UnityEditor`, GUID population is done via an editor utility:

- Select one or more manifest assets
- **Tools → Coreloop AssetManifest → Sync GUIDs (Selected)**

## Runtime lookup

```csharp
manifest.Initialize();

if (manifest.TryGetAsset(guid, out var asset))
{
    // use asset
}
```
