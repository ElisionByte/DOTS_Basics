using Player.Characters.States;
using UnityEngine;


namespace Player.Characters
{
    public abstract class Character : MonoBehaviour
    {
        public CharacterController Controller = default;
        public Animator Animator;
        public float Speed;

        [HideInInspector] public Vector3 MoveDirection;
        [HideInInspector] public Vector2 RotationDirection;

        [SerializeField] protected float gravity = 19.81f;
        [SerializeField] protected float turnSmothTime = 0.2f;
        [SerializeField] protected float jumpSpeed = 20f;

        protected State currentState;

        public abstract void JumpCheck();
        public abstract void RotateCheck();

        public void SetState(State state)
        {
            currentState?.OnStateExit();
            currentState = state;
            currentState?.OnStateEnter();
        }
    }
}