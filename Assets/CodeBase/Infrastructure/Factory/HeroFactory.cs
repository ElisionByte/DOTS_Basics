using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Logic.Hero;
using CodeBase.Services.Gravity;

using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class HeroFactory : IHeroFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IInputService _inputService;
        private readonly IGravityService _gravityService;

        public HeroFactory(IAssetProvider assetProvider, IInputService inputService, IGravityService gravityService)
        {
            _assetProvider = assetProvider;
            _inputService = inputService;
            _gravityService = gravityService;
        }

        public GameObject CreateHero(GameObject initialPoint)
        {
            GameObject hero = _assetProvider.Instantiate(AssetPaths.mannequinTestHero, at: initialPoint.transform.position);
            ConstructHero(hero);
            return hero;
        }

        private void ConstructHero(GameObject hero)
        {
            hero.GetComponent<HeroDisplaycement>()?.Construct(_inputService,_gravityService);
            hero.GetComponentInChildren<HeroForwardRotate>()?.Construct(_inputService);
        }
    }
}