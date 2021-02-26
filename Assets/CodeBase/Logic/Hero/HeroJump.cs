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

        public CollisionDetector collisionDetector;

        private int _jumpsCount;
        private Vector3 _currentVelocity;

        private IInputService _inputService;
        private IPhysicsService _physicsService;

        public void Construct(IInputService inputService, IPhysicsService physicsService)
        {
            _inputService = inputService;
            _physicsService = physicsService;
        }

        private void Update()
        {
            if (_inputService.IsJumpPressed)
            {
                _currentVelocity = _physicsService.RBVelocity;
                Jump();
                _physicsService.RBVelocity = _currentVelocity;
            }
        }

        private void FixedUpdate()
        {
            if (collisionDetector.IsGrounded || collisionDetector.IsClimbing)
            {
                _jumpsCount = 0;
                collisionDetector.ContactNormal = Vector3.zero;
            }
            else
                _physicsService.RBVelocityY += -9.81f * Time.deltaTime;
            
        }

        private void Jump()
        {
            Vector3 jumpDirection;

            if (collisionDetector.IsGrounded || collisionDetector.IsClimbing)
                jumpDirection = collisionDetector.ContactNormal;
            else if (_jumpsCount < airJumpsCount)
                jumpDirection = Vector3.up;
            else return;

            _jumpsCount += 1;

            float jumpSpeed = Mathf.Sqrt(2f * Vector3.up.magnitude * jumpHeight);

           // jumpDirection = (jumpDirection.normalized;

            float alignedSpeed = Vector3.Dot(_currentVelocity, jumpDirection);

            if (alignedSpeed > 0f)
                jumpSpeed = Math.Max(jumpSpeed - alignedSpeed, 0f);

            if (_currentVelocity.y < 0)
                _currentVelocity.y = 0;

            _currentVelocity += jumpDirection.normalized * jumpSpeed;
        }
    }
}