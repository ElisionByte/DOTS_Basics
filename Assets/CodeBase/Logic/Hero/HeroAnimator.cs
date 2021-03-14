using System;

using CodeBase.Logic.Hero.Animation;

using UnityEngine;


namespace CodeBase.Logic.Hero
{
    [RequireComponent(typeof(Animator))]
    public class HeroAnimator : MonoBehaviour, IAnimationStateReader
    {
        public Animator animator;
        public Rigidbody heroRigidbody;

        private int _horizontalSpeedHash = Animator.StringToHash("HorizontalSpeed");
        private int _verticalSpeedHash = Animator.StringToHash("VerticalSpeed");

        private int _groundRunHash = Animator.StringToHash("GroundRun");
        private int _wallRunHash = Animator.StringToHash("WallRun");
        private int _jumpHash = Animator.StringToHash("Jump");
        private int _flyHash = Animator.StringToHash("IsFly");

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public AnimatorState State { get; private set; }
        private void Update()
        {
            Vector3 heroVelocity = heroRigidbody.velocity;

            animator.SetFloat(_horizontalSpeedHash, Mathf.Abs(heroVelocity.x) + Mathf.Abs(heroVelocity.z), 0.1f, Time.deltaTime);

            animator.SetFloat(_verticalSpeedHash, heroVelocity.y, 0.1f, Time.deltaTime);

            if (heroVelocity.y > Constants.Epsilone || heroVelocity.y < -Constants.Epsilone)
                PlayFly();
        }

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }
        public void ExitedState(int stateHash)
        {
            StateExited?.Invoke(StateFor(stateHash));
        }
        public void PlayWallRun() => animator.SetTrigger(_wallRunHash);

        public void PlayGroundRun() => animator.SetTrigger(_groundRunHash);

        public void PlayJump() => animator.SetTrigger(_jumpHash);

        public void StopJump()
        {
            animator.ResetTrigger(_jumpHash);
            StopFly();
        }

        private void PlayFly() => animator.SetTrigger(_flyHash);
        private void StopFly() => animator.ResetTrigger(_flyHash);

        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState resultState;

            if (stateHash == _groundRunHash)
                resultState = AnimatorState.GroundRun;
            else if (stateHash == _wallRunHash)
                resultState = AnimatorState.WallRun;
            else
                resultState = AnimatorState.Jump;
            Debug.Log(resultState);
            return resultState;
        }
    }
}