using Cinemachine;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    public class CustomizationWindow : Window
    {
        public GameObject mainCanvas;
        public new CinemachineVirtualCamera camera;

        private CinemachineTrackedDolly _dollyTrack;

        private void Start()
        {
            UIAnimator.AlphaEffect(mainCanvas, 0f, Effect.Hide);
            _dollyTrack = camera.GetCinemachineComponent<CinemachineTrackedDolly>();
        }

        public override void Close()
        {
            UIAnimator.AlphaEffect(mainCanvas, 1f, Effect.Hide);
            _dollyTrack.m_PathPosition = 0;
        }

        public override void Open()
        {
            UIAnimator.AlphaEffect(mainCanvas, 2f, Effect.Show);
            _dollyTrack.m_PathPosition = 2;
        }
    }
}
