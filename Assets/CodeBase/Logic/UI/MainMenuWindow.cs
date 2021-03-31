using CodeBase.Infrastructure.StateMachine;

using UnityEngine.UI;

namespace CodeBase.Logic.UI
{
    public class MainMenuWindow : Window
    {
        private const string _level1 = "Level1";

        public Button StartGame;
        public Button OpenSettings;

        private GameStateMachine _gameStateMachine;
        private IHandlerUIService _handlerUIService;

        public void Construct(GameStateMachine gameStateMachine,IHandlerUIService handlerUIService)
        {
            _gameStateMachine = gameStateMachine;
            _handlerUIService = handlerUIService;

            StartGame.onClick.AddListener(OnStartGame);
            OpenSettings.onClick.AddListener(OnOpenSettings);
        }

        private void OnDestroy()
        {
            StartGame.onClick.RemoveListener(Close);
            OpenSettings.onClick.RemoveListener(OnOpenSettings);
        }

        public override void Close()
        {
            gameObject.SetActive(false);
        }
        public override void Open()
        {
            gameObject.SetActive(true);
        }

        private void OnOpenSettings() => _handlerUIService.Open(_handlerUIService.SettingsWindow);
        private void OnStartGame()
        {
            _gameStateMachine.Enter<LoadLevelState, string>(_level1);
            Close();
        }
    }
}