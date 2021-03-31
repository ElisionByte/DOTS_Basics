using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.StateMachine;
using CodeBase.Logic.UI;
using CodeBase.Services;

using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
    public class FactoryUI : IFactoryUIService
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IAssetProvider _assetProvider;
        private readonly IHandlerUIService _handlerUIService;

        public FactoryUI(GameStateMachine gameStateMachine, IAssetProvider assetProvider, IHandlerUIService handlerUIService)
        {
            _gameStateMachine = gameStateMachine;
            _assetProvider = assetProvider;
            _handlerUIService = handlerUIService;
        }

        private MainMenuWindow CreateMainMenuWindow()
        {
            MainMenuWindow mainMenuWindow = _assetProvider.Instantiate(AssetPaths.MainMenuWindowPath, Vector3.zero).GetComponent<MainMenuWindow>();
            mainMenuWindow.Construct(_gameStateMachine,_handlerUIService);
            _handlerUIService.MainMenuWindow = mainMenuWindow;
            return mainMenuWindow;
        }
        private SettingsWindow CreataSettingsWindow(MainMenuWindow mainMenuWindow)
        {
            SettingsWindow settingsWindow = _assetProvider.Instantiate(AssetPaths.SettingsWindowPath, mainMenuWindow.transform, Vector3.zero).GetComponent<SettingsWindow>();
            settingsWindow.Construct();
            _handlerUIService.SettingsWindow = settingsWindow;
            return settingsWindow;
        }

        public void CreateMainMenu()
        {
            MainMenuWindow mainMenuWindow = CreateMainMenuWindow();
            CreataSettingsWindow(mainMenuWindow);
        }
    }

    public interface IFactoryUIService : IService
    {
        void CreateMainMenu();
    }
}