using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Logic.Hero;

using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class HeroFactory : IHeroFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IInputService _inputService;

        public HeroFactory(IAssetProvider assetProvider,IInputService inputService)
        {
            _assetProvider = assetProvider;
            _inputService = inputService;
        }

        public GameObject CreateHero(GameObject initialPoint)
        {
            GameObject hero = _assetProvider.Instantiate(AssetPaths.mannequinTestHero, at: initialPoint.transform.position);
            ConstractHero(hero);
            return hero;
        }

        private void ConstractHero(GameObject hero)
        {
            hero.GetComponent<HeroMove>()?.Construct(_inputService);
            hero.GetComponent<HeroJump>()?.Construct(_inputService);
            hero.GetComponent<HeroForwardRotate>()?.Construct(_inputService);
        }
    }
}
