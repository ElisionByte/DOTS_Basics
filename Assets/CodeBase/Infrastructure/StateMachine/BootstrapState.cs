using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factories;
using CodeBase.Logic.JobSystems.Map;
using CodeBase.Logic.UI;
using CodeBase.Services;
using CodeBase.Services.Gravity;
using CodeBase.Services.Inputs;
using CodeBase.Services.Map;
using CodeBase.Services.Physics;

using UnityEngine;

namespace CodeBase.Infrastructure.StateMachine
{
    public class BootstrapState : IState
    {
        private const string _initialSceneName = "Initial";
        private const string _afterLoadSceneName = "Menu";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(_initialSceneName, onLoaded: EnterLoadLevelState);
        }
        public void Exit()
        {

        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(InputService());
            _services.RegisterSingle<IHandlerUIService>(new HandlerUIService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IPhysicsService>(new PhysicsService());
            _services.RegisterSingle<IMapJobsSystemService>(new MapJobsSystemService());

            _services.RegisterSingle<IFactoryUIService>(new FactoryUI(
                _gameStateMachine,
                _services.Single<IAssetProvider>(),
                _services.Single<IHandlerUIService>()
                ));

            _services.RegisterSingle<IMapFactory>(new MapFactory(
                    _services.Single<IAssetProvider>(),
                    _services.Single<IMapJobsSystemService>()
                    ));
            _services.RegisterSingle<IHeroFactory>(new HeroFactory(
                    _services.Single<IAssetProvider>(),
                    _services.Single<IInputService>(),
                    _services.Single<IGravityService>(),
                    _services.Single<IPhysicsService>()
                    ));
        }

        private IInputService InputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new StandaloneInputService();
        }
        private void EnterLoadLevelState() =>
            _gameStateMachine.Enter<LoadMenuState, string>(_afterLoadSceneName);
    }
}