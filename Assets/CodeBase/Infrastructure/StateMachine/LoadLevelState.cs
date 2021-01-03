using CodeBase.Infrastructure.Factory;

using UnityEngine;

namespace CodeBase.Infrastructure.StateMachine
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly SceneLoader sceneLoader;
        private readonly IGameFactory gameFactory;
        private readonly LoadingCurtain loadingCurtain;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IGameFactory gameFactory, LoadingCurtain loadingCurtain)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
            this.gameFactory = gameFactory;
            this.loadingCurtain = loadingCurtain;
        }

        public void Enter(string sceneName)
        {
            loadingCurtain.Open();
            sceneLoader.Load(sceneName, onLoaded: OnCompletedLoad);
        }

        public void Exit()
        {
            Debug.Log("Exit load level State");
        }

        private void OnCompletedLoad()
        {
            InitGameWorld();
            loadingCurtain.Close();
        }
        private void InitGameWorld()
        {
            gameFactory.CreateHero(GameObject.FindGameObjectWithTag("InitPoint"));
        }
    }
}