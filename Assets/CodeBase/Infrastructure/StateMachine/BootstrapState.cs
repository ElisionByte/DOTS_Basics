using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Services.Inputs;

using UnityEngine;

namespace CodeBase.Infrastructure.StateMachine
{
    public class BootstrapState : IState
    {
        private const string _initialSceneName = "Initial";
        //Will be main menu
        private const string _afterLoadSceneName = "Level1";

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
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IHeroFactory>(new HeroFactory(_services.Single<IAssetProvider>(),_services.Single<IInputService>()));
        }

        private IInputService InputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();

            Debug.LogError("Wrong Input Service, will returned null");
            return null;
        }
        private void EnterLoadLevelState() =>
            _gameStateMachine.Enter<LoadLevelState, string>(_afterLoadSceneName);
    }
}