using UnityEngine;

namespace CodeBase.Logic.Hero.CameraBehaviour
{
    public class HeroCamera : MonoBehaviour
    {
        public Transform rootTransform;
        public new Camera camera;

        private void LateUpdate() => 
            rootTransform.rotation = Quaternion.Euler(UpVector());

        private Vector3 UpVector() =>
            new Vector3(0, camera.transform.rotation.eulerAngles.y, 0);
    }
}