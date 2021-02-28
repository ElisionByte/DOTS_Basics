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
        private int _stepsSinseLastJump;

        private IInputService _inputService;
        private IPhysicsService _physicsService;
        private bool _isJumping;

        public void Construct(IInputService inputService, IPhysicsService physicsService)
        {
            _inputService = inputService;
            _physicsService = physicsService;
        }

        private void Update()
        {
            _isJumping = _inputService.IsJumpPressed;
            if (_isJumping)
            {
                Jump();
            }
        }

        private void FixedUpdate()
        {
            if (collisionDetector.IsGrounded || collisionDetector.IsClimbing)
            {
                _jumpsCount = 0;
                if (_stepsSinseLastJump >= 2)
                {
                    _physicsService.RBVelocityY = 0f;
                    _physicsService.RBJumpVelocity = Vector3.zero;
                }   
            }
            else
                _physicsService.RBVelocityY += -9.81f * Time.deltaTime;

            _stepsSinseLastJump++;
        }

        private void Jump()
        {
            Vector3 _jumpVelocity;

            if (collisionDetector.IsGrounded)
            {
                _jumpVelocity = collisionDetector.ContactNormal;
            }
            else if (collisionDetector.IsClimbing)
            {
                _jumpVelocity = collisionDetector.ContactNormal;
                _jumpVelocity += Vector3.up;
            }
            else if (_jumpsCount < airJumpsCount)
                _jumpVelocity = Vector3.up;
            else
                return;

            _jumpsCount += 1;
            _stepsSinseLastJump = 0;

            float jumpSpeed = Mathf.Sqrt(2f * _jumpVelocity.magnitude * jumpHeight);

            if (_physicsService.RBVelocityY < 0)
                _physicsService.RBVelocityY = 0;

            _physicsService.RBJumpVelocity = _jumpVelocity.normalized * jumpSpeed;
        }
    }
}