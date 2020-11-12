using AnimationHandlers;
using UnityEngine;

namespace Assets.Scripts
{
    public class SphereBullet : MonoBehaviour
    {
        public float emergenceTime;

        public void SetPosition(Vector3 position)
        {
            this.gameObject.transform.position = position;
        }
        public void Show()
        {
            ObjectAnimator.UpScale(this.gameObject, emergenceTime);
        }
    }
}