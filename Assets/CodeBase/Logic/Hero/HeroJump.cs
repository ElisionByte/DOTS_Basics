using System;

using CodeBase.Services.Physics;

using UnityEngine;

namespace CodeBase.Logic.Hero
{
    [RequireComponent(typeof(Rigidbody))]
    public class HeroJump : MonoBehaviour
    {
        [Range(1f, 20f)] public float jumpHeight;
        [Range(0, 5)] public int airJumpsCount;

        public GroundChecker groundChecker;

        private int _jumpsCount;
        private Vector3 _currentVelocity;

        private IInputService _inputService;
        private IPhysicsService _physicsDisplaycementService;

        public void Construct(IInputService inputService, IPhysicsService physicsDisplaycementService)
        {
            _inputService = inputService;
            _physicsDisplaycementService = physicsDisplaycementService;
        }

        private void Update()
        {
            if (_inputService.IsJumpPressed)
            {
                Debug.Log("Jump");
                _currentVelocity = _physicsDisplaycementService.Velocity;
                Jump();
                _physicsDisplaycementService.Velocity = _currentVelocity;
            }
        }

        private void FixedUpdate()
        {
            if (groundChecker.IsGrounded)
            {
                _jumpsCount = 0;
            }
        }

        private void Jump()
        {
            if (_jumpsCount > airJumpsCount)
                return;

            Vector3 jumpDirection = Vector3.up;

            _jumpsCount += 1;

            float jumpSpeed = Mathf.Sqrt(2f * Vector3.down.magnitude * jumpHeight);
            float alignedSpeed = Vector3.Dot(_currentVelocity, jumpDirection);

            if (alignedSpeed > 0f)
                jumpSpeed = Math.Max(jumpSpeed - alignedSpeed, 0f);

            if(_currentVelocity.y<0)
                _currentVelocity.y = 0;
            _currentVelocity += jumpDirection * jumpSpeed;
        }
    }
}