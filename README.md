# Coreloop AssetManifest

A small, generic ScriptableObject-based manifest that maps assets to stable GUID strings.

## Install (UPM via Git URL)

In Unity **Package Manager** → **Add package from git URL...**:

- `https://github.com/<owner>/<repo>.git?path=/Packages/com.betopia.core.manifest`

## Basic usage

1. Create your own concrete manifest type:

```csharp
using UnityEngine;

namespace Coreloop.AssetManifest
{
    [CreateAssetMenu(menuName = "Coreloop/AssetManifest/TextAsset Manifest", fileName = "TextAssetManifest")]
    public class TextAssetManifest : AssetManifest<TextAsset> { }
}
```

2. Add assets to the manifest `entries` list.
3. In the editor, run **Tools → Coreloop AssetManifest → Sync GUIDs (Selected)** to populate GUIDs.

See the included sample in Package Manager → this package → **Samples**.
