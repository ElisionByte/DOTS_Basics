using CodeBase.Services;

namespace CodeBase.Logic.UI
{
    public interface IHandlerUIService : IService
    {
        SettingsWindow SettingsWindow { get; set; }
        MainMenuWindow MainMenuWindow { get; set; }

        void Open(Window window);
        void Close(Window window);
    }
}