using CodeBase.Infrastructure.Factory;

using UnityEngine;

namespace CodeBase.Infrastructure.StateMachine
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string _initPointTag = "HeroInitPoint";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IHeroFactory _gameFactory;
        private readonly LoadingCurtain _loadingCurtain;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IHeroFactory gameFactory, LoadingCurtain loadingCurtain)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Open();
            _sceneLoader.Load(sceneName, onLoaded: OnCompletedLoad);
        }

        public void Exit()
        {
            Debug.Log("Exit load level State");
        }

        private void OnCompletedLoad()
        {
            InitGameWorld();
            _loadingCurtain.Close();
        }
        private void InitGameWorld()
        {
            _gameFactory.CreateHero(GameObject.FindGameObjectWithTag(_initPointTag));
        }
    }
}