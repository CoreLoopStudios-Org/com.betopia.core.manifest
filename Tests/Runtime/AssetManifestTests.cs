using NUnit.Framework;
using UnityEngine;

namespace Coreloop.AssetManifest.Tests
{
    public class AssetManifestTests
    {
        private class TestTextAssetManifest : AssetManifest<TextAsset> { }

        [Test]
        public void TryGetAsset_FindsByGuid()
        {
            var manifest = ScriptableObject.CreateInstance<TestTextAssetManifest>();
            var asset = new TextAsset("hello");

            // Populate the private serialized list via reflection (runtime-safe).
            var field = typeof(AssetManifest<TextAsset>).GetField("entries", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var list = new System.Collections.Generic.List<ManifestEntry<TextAsset>>
            {
                new() { asset = asset, guid = "guid-1" }
            };
            field!.SetValue(manifest, list);

            Assert.IsTrue(manifest.TryGetAsset("guid-1", out var found));
            Assert.AreSame(asset, found);
        }
    }
}
