
namespace CodeBase.Logic.UI
{
    public class HandlerUIService : IHandlerUIService
    {
        private  MainMenuWindow _mainMenuWindow;
        private  SettingsWindow _settingsWindow;

        public SettingsWindow SettingsWindow 
        {
            get => _settingsWindow;
            set => _settingsWindow = value;
        } 
        public MainMenuWindow MainMenuWindow
        {
            get => _mainMenuWindow;
            set => _mainMenuWindow = value;
        }

        public void Close(Window window)
        {
            window.Close();
        }
        public void Open(Window window)
        {
            window.Open();
        }
    }
}