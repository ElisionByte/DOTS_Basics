using CodeBase.Infrastructure.Factories;

namespace CodeBase.Infrastructure.StateMachine
{
    internal class LoadMenuState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IFactoryUIService _factoryUIService;
        private readonly LoadingCurtain _loadingCurtain;

        public LoadMenuState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IFactoryUIService factoryUIService,LoadingCurtain loadingCurtain)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _factoryUIService = factoryUIService;
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
            InitMenu();
            _loadingCurtain.Close();
        }
        private void InitMenu()
        {
            _factoryUIService.CreateMainMenu();
        }
    }
}