using System;
using UnityEditor;
using UnityEngine;

namespace Coreloop.AssetManifest.Editor.Editor
{
    public static class AssetManifestGuidSyncUtility
    {
        [MenuItem("Tools/Coreloop AssetManifest/Sync GUIDs (Selected)")]
        private static void SyncSelected()
        {
            foreach (var obj in Selection.objects)
            {
                if (obj is not ScriptableObject so) continue;
                if (!IsAssetManifest(so.GetType())) continue;

                SyncOne(so);
            }
        }

        private static bool IsAssetManifest(Type type)
        {
            while (type != null)
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(AssetManifest<>))
                    return true;

                type = type.BaseType;
            }

            return false;
        }

        private static void SyncOne(ScriptableObject manifest)
        {
            var serializedObject = new SerializedObject(manifest);
            var entriesProperty = serializedObject.FindProperty("entries");

            if (entriesProperty == null || !entriesProperty.isArray)
                return;

            bool dirty = false;

            for (int i = 0; i < entriesProperty.arraySize; i++)
            {
                var entryProperty = entriesProperty.GetArrayElementAtIndex(i);
                var assetProperty = entryProperty.FindPropertyRelative("asset");
                var guidProperty = entryProperty.FindPropertyRelative("guid");

                var asset = assetProperty.objectReferenceValue;
                var guid = asset != null
                    ? AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(asset))
                    : string.Empty;

                if (guidProperty.stringValue != guid)
                {
                    guidProperty.stringValue = guid;
                    dirty = true;
                }
            }

            if (!dirty)
                return;

            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(manifest);
        }
    }
}
