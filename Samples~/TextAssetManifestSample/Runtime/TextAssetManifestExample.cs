using UnityEngine;

namespace Coreloop.AssetManifest
{
    public class TextAssetManifestExample : MonoBehaviour
    {
        [SerializeField] private TextAssetManifest manifest;

        [Tooltip("Fill this with the GUID after running Tools/Coreloop AssetManifest/Sync GUIDs (Selected) on the manifest asset.")]
        [SerializeField] private string textAssetGuid;

        private void Start()
        {
            if (manifest == null || string.IsNullOrEmpty(textAssetGuid))
                return;

            if (manifest.TryGetAsset(textAssetGuid, out var asset) && asset != null)
                Debug.Log(asset.text);
        }
    }
}
