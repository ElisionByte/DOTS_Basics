using UnityEngine;


namespace CodeBase.Logic.Hero
{
    [RequireComponent(typeof(Animator))]
    public class HeroAnimator : MonoBehaviour
    {
        public Animator animator;
        public Rigidbody heroRigidbody;

        private int _speed = Animator.StringToHash("Speed");

        private void Update()
        {
            animator.SetFloat(_speed, heroRigidbody.velocity.magnitude);
        }
    }
}