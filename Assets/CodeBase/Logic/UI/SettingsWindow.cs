using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Logic.UI
{
    public class SettingsWindow : Window
    {
        public Button Hide;

        public void Construct()
        {
            Hide.onClick.AddListener(Close);
            gameObject.transform.localPosition = Vector3.zero;
        }
        private void OnDestroy()
        {
            Hide.onClick.RemoveListener(Close);
        }

        public override void Close()
        {
            gameObject.SetActive(false);
        }
        public override void Open()
        {
            gameObject.SetActive(true);
        }
    }
}