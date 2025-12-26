using System;
using System.Collections.Generic;
using UnityEngine;

namespace Coreloop.AssetManifest
{
    [Serializable]
    public struct ManifestEntry<T> where T : UnityEngine.Object
    {
        [Tooltip("The actual asset reference.")]
        public T asset;

        [Tooltip("Auto-filled Unity GUID. Stable across renames and moves.")]
        [ReadOnly]
        public string guid;
    }

    
    public abstract class AssetManifest<T> : ScriptableObject where T : UnityEngine.Object
    {
        [SerializeField] private List<ManifestEntry<T>> entries = new();

        private Dictionary<string, T> _guidToAsset;
        private Dictionary<T, string> _assetToGuid;

        private void OnEnable() => Initialize();
        
        public bool TryGetAsset(string guid, out T asset) => _guidToAsset.TryGetValue(guid, out asset);

        public bool TryGetGuid(T asset, out string guid) => _assetToGuid.TryGetValue(asset, out guid);

        private void Initialize()
        {
            _guidToAsset = new Dictionary<string, T>(entries.Count);
            _assetToGuid = new Dictionary<T, string>(entries.Count);

            foreach (var entry in entries)
            {
                if (entry.asset == null || string.IsNullOrEmpty(entry.guid)) continue;
                if (!_guidToAsset.ContainsKey(entry.guid)) _guidToAsset.Add(entry.guid, entry.asset);
                if (!_assetToGuid.ContainsKey(entry.asset)) _assetToGuid.Add(entry.asset, entry.guid);
            }
        }

        private void Reset() => Initialize();
    }

    public class ReadOnlyAttribute : PropertyAttribute { }
}