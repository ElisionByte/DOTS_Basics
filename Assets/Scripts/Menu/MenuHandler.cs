using UnityEngine;

namespace Assets.Scripts.Menu
{
    public class MenuHandler : MonoBehaviour
    {
        [SerializeField] private MainMenuWindow _mainMenuWindow = default;
        [SerializeField] private CustomizationWindow _customizationWindow = default;

        public void OpenCustomizationWindow()
        {
            _mainMenuWindow.Close();
            _customizationWindow.Open();
        }
        public void CloseCustomizationWindow()
        {
            _customizationWindow.Close();
            _mainMenuWindow.Open();
        }
    }
}

