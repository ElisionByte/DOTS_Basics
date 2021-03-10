using System;
using System.Collections.Generic;
using System.Linq;

using CodeBase.Logic.Map;

using UnityEditor;
using UnityEditor.SceneManagement;

using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(UniqueID))]
    class UniqueIDEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            var uniqueID = (UniqueID)target;

            if (IsPrefab(uniqueID))
                return;

            if (string.IsNullOrEmpty(uniqueID.ID))
                Generate(uniqueID);
            else 
            {
                UniqueID[] uniqueIDs = FindObjectsOfType<UniqueID>();

                if (uniqueIDs.Any(other => other != uniqueID && other.ID == uniqueID.ID))
                {
                    Generate(uniqueID);
                    return;
                }

                if (EqualsWithPrefabsID(uniqueID.ID))
                    Generate(uniqueID);
            }
        }


        private bool IsPrefab(UniqueID uniqueID) =>
            PrefabUtility.IsPartOfPrefabAsset(uniqueID);

        private void Generate(UniqueID uniqueID)
        {
            uniqueID.ID = $"{uniqueID.gameObject.scene.name}_{Guid.NewGuid()}_{DateTime.Now.Ticks}";

            if (!Application.isPlaying)
            {
                EditorUtility.SetDirty(uniqueID);
                EditorSceneManager.MarkSceneDirty(uniqueID.gameObject.scene);
            }
        }

        private bool EqualsWithPrefabsID(string uniqueID)
        {
            IEnumerable<UniqueID> ids = LoadPrefabsContaining<UniqueID>();
            return ids.Any(id => id.ID == uniqueID);
        }

        private IEnumerable<T> LoadPrefabsContaining<T>() where T : Component
        {
            var guids = AssetDatabase.FindAssets("t:Prefab");
            var paths = guids.Select(AssetDatabase.GUIDToAssetPath);
            var objs = paths.Select(AssetDatabase.LoadAssetAtPath<T>);
            var components = objs.Where(component => component);
            return components;
        }
    }
}