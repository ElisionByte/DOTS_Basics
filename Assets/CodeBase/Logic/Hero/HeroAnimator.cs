using UnityEngine;


namespace CodeBase.Logic.Hero
{
    [RequireComponent(typeof(Animator))]
    public class HeroAnimator : MonoBehaviour
    {
        public Animator animator;
        public Rigidbody rigidbody;

        private int _speed = Animator.StringToHash("Speed");

        private void Update()
        {
            animator.SetFloat(_speed, rigidbody.velocity.magnitude, 0.1f, Time.deltaTime);
        }
    }
}