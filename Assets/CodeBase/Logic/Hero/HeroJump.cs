using UnityEngine;

namespace CodeBase.Logic.Hero
{
    [RequireComponent(typeof(Rigidbody))]
    public class HeroJump : MonoBehaviour
    {
        public Rigidbody heroRigidbody;
        public GroundCheker groundCheker;

        [Range(1f, 30f)] public float jumpHeight;
        [Range(0, 5)] public int maxAirJumps;

        private int _jumpPhase;
        private bool _desiredJump;
        private Vector3 _currentVelocity;

        private IInputService _inputService;

        public void Construct(IInputService inputService) =>
                _inputService = inputService;

        private void Update() =>
            _desiredJump |= _inputService.IsJumpPressed;
        private void FixedUpdate()
        {
            _currentVelocity = heroRigidbody.velocity;

            if (groundCheker.OnGround)
            {
                _jumpPhase = 0;
                if (groundCheker.GroundContactCount > 1)
                    groundCheker.ContactNormal.Normalize();
            }
            else
                groundCheker.ContactNormal = Vector3.up;

            if (_desiredJump)
            {
                _desiredJump = false;
                Jump();
            }

            heroRigidbody.velocity = _currentVelocity;
        }

        private void Jump()
        {
            if (groundCheker.OnGround || _jumpPhase < maxAirJumps)
            {
                _jumpPhase += 1;
                float jumpSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
                float alignedSpeed = Vector3.Dot(_currentVelocity, groundCheker.ContactNormal);
                if (alignedSpeed > 0f)
                    jumpSpeed = Mathf.Max(jumpSpeed - alignedSpeed, 0f);

                _currentVelocity += groundCheker.ContactNormal * jumpSpeed;
            }
        }
    }
}