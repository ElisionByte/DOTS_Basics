using CodeBase.Infrastructure.Factories;
using CodeBase.Logic.Map;

using UnityEngine;

namespace CodeBase.Infrastructure.StateMachine
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string _initPointTag = "HeroInitPoint";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IHeroFactory _gameFactory;
        private readonly IMapFactory _mapFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IHeroFactory gameFactory,IMapFactory mapFactory, LoadingCurtain loadingCurtain)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _mapFactory = mapFactory;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Open();
            _sceneLoader.Load(sceneName, onLoaded: OnCompletedLoad);
        }

        public void Exit()
        {
        
        }

        private void OnCompletedLoad()
        {
            InitLevel();
            _loadingCurtain.Close();
        }
        private void InitLevel()
        {
            MapPropSpawner[] spawners =  GameObject.FindObjectsOfType<MapPropSpawner>();
            foreach (MapPropSpawner spawner in spawners)
            {
                spawner.Construct(_mapFactory);
                spawner.Spawn();
            }
            _mapFactory.CreatePropNotificator();
            _gameFactory.CreateHero(GameObject.FindGameObjectWithTag(_initPointTag));
        }
    }
}