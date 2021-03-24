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

        private int _verticalSpeedHash = Animator.StringToHash("VerticalSpeed");

        private int _inputXHash = Animator.StringToHash("InputX");
        private int _inputYHash = Animator.StringToHash("InputY");

        private int _groundRunHash = Animator.StringToHash("GroundMove");
        private int _wallRunHash = Animator.StringToHash("WallMove");
        private int _jumpHash = Animator.StringToHash("FlyingMove");
        private int _flyHash = Animator.StringToHash("IsFly");

        private IInputService _inputService;
        private HeroRotator _heroRotator;

        private event Action<AnimatorState> StateEntered;
        private event Action<AnimatorState> StateExited;

        public AnimatorState State { get; private set; }

        public void Construct(IInputService inputService,HeroRotator heroRotator)
        {
            _inputService = inputService;
            _heroRotator = heroRotator;
            StateEntered += InvokeAllAboutStateEnter;
        }

        private void Update()
        {
            Vector3 heroVelocity = heroRigidbody.velocity;

            SetInputXY(_inputService.Axis);

            animator.SetFloat(_verticalSpeedHash, heroVelocity.y, 0.1f, Time.deltaTime);

            if (heroVelocity.y > Constants.Epsilone || heroVelocity.y < -Constants.Epsilone)
            {
                PlayFly();
            } 
        }
        private void OnDestroy()
        {
            StateEntered -= InvokeAllAboutStateEnter;
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

        private void SetInputXY(Vector2 axis)
        {
            animator.SetFloat(_inputXHash, axis.x, 0.1f, Time.deltaTime);
            animator.SetFloat(_inputYHash, axis.y, 0.1f, Time.deltaTime);
        }
        private void PlayFly() => animator.SetTrigger(_flyHash);
        private void StopFly() => animator.ResetTrigger(_flyHash);
        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState resultState = default;

            if (stateHash == _groundRunHash)
                resultState = AnimatorState.GroundRun;
            else if (stateHash == _wallRunHash)
                resultState = AnimatorState.WallRun;
            else if(stateHash == _jumpHash)
                resultState = AnimatorState.Jump;

            return resultState;
        }
        private void InvokeAllAboutStateEnter(AnimatorState state)
        {
            _heroRotator.AnimatorState = state;
        }
    }
}