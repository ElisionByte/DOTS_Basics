using System;

using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.StateMachine
{
    public class BootstrapState : IState
    {
        private const string initialSceneName = "Initial";

        private readonly GameStateMachine gameStateMachine;
        private readonly SceneLoader sceneLoader;
        private readonly AllServices services;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
            this.services = services;
            RegisterServices();
        }

        public void Enter()
        {
            sceneLoader.Load(initialSceneName, onLoaded: EnterLoadLevelState);
        }
        public void Exit()
        {

        }

        private void RegisterServices()
        {
            services.RegisterSingle<IAssetProvider>(new AssetProvider());
            services.RegisterSingle<IGameFactory>(new GameFactory(services.Single<IAssetProvider>()));
        }
        private void EnterLoadLevelState() => 
            gameStateMachine.Enter<LoadLevelState,string>("Development");
    }
}