using CodeBase.Infrastructure.AssetManagement;

using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        private void Start()
        {
            if (FindObjectOfType<GameBootstraper>() == null)
            {
                Instantiate(Resources.Load(AssetPaths.gameBootstraperPath));
            }
        }
    }
}