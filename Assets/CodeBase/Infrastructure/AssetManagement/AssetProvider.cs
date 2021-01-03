using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string path) =>
            Object.Instantiate(LoadPrefabFromResources(path));
        public GameObject Instantiate(string path, Vector3 at) =>
            Object.Instantiate(LoadPrefabFromResources(path), at, Quaternion.identity);
        public GameObject Instantiate(string path, Transform parent, Vector3 at) =>
            Object.Instantiate(LoadPrefabFromResources(path), at, Quaternion.identity, parent);

        private GameObject LoadPrefabFromResources(string path)
        {
            return Resources.Load<GameObject>(path);
        }
    }
}
