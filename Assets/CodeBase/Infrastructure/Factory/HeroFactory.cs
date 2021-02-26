using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Logic.Hero;
using CodeBase.Services.Physics;
using CodeBase.Services.Gravity;

using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class HeroFactory : IHeroFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IInputService _inputService;
        private readonly IGravityService _gravityService;
        private readonly IPhysicsService _physicsService;

        public HeroFactory(IAssetProvider assetProvider, IInputService inputService, IGravityService gravityService, IPhysicsService physicsDisplaycementService)
        {
            _assetProvider = assetProvider;
            _inputService = inputService;
            _gravityService = gravityService;
            _physicsService = physicsDisplaycementService;
        }

        public GameObject CreateHero(GameObject initialPoint)
        {
            GameObject hero = _assetProvider.Instantiate(AssetPaths.mannequinTestHero, at: initialPoint.transform.position);
            ConstructHero(hero);
            return hero;
        }

        private void ConstructHero(GameObject hero)
        {
            _physicsService.SetRigidbody(hero.GetComponent<Rigidbody>());
            hero.GetComponentInChildren<HeroForwardRotate>()?.Construct(_inputService);
            hero.GetComponent<HeroMove>()?.Construct(_inputService, _physicsService);
            hero.GetComponent<HeroJump>()?.Construct(_inputService, _physicsService);
            hero.GetComponent<CollisionDetector>().Construct(_physicsService);
        }
    }
}