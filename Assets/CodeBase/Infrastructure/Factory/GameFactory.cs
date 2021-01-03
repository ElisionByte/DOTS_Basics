using CodeBase.Infrastructure.AssetManagement;

using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    //Hero Factory need some hero type and create on type depend
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider assetProvider;

        public GameFactory(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }

        public GameObject CreateHero(GameObject initialPoint) => 
            assetProvider.Instantiate(AssetPaths.robotKylePath, at: initialPoint.transform.position);
    }
}
