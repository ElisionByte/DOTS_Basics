using UnityEngine;

namespace Assets.Scripts.Menu
{
    public class MainMenuWindow : Window
    {
        public GameObject mainCanvas;

        public override void Close()
        {
            UIAnimator.AlphaEffect(mainCanvas, 1f, Effect.Hide);
        }

        public override void Open()
        {
            UIAnimator.AlphaEffect(mainCanvas, 2f, Effect.Show);
        }
    }
}