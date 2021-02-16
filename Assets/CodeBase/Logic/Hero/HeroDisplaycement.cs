using CodeBase.Services.Gravity;

using UnityEngine;

namespace CodeBase.Logic.Hero
{
    [RequireComponent(typeof(Rigidbody))]
    public class HeroDisplaycement : MonoBehaviour
    {
        public Rigidbody heroRigidbody;

        [Range(1f, 30f)] public float maxSpeed;
        [Range(1f, 30f)] public float maxGroundAcceleration;
        [Range(1f, 30f)] public float maxAirAcceleration;
        [Range(1f, 30f)] public float jumpHeight;
        [Range(0f, 50f)] public float maxSnapSpeed = 100f;
        [Range(0f, 90f)] public float maxGroundAngle = 25f, maxStairsAngle = 50f;
        [Range(90f, 180f)] public float maxClimbAngle = 140f;

        [Range(0, 5)] public int maxAirJumps;
        [Min(0f)] public float probeDistance = 1f;
        public LayerMask probeMask = -1, stairsMask = -1, climbMask = -1;

        private bool _desiredJump;
        private int _jumpsCount;
        private int _stepsSinceLastJump, _stepsSinceLastGrounded;
        private int _groundContactCount, _steepContactCount;
        private float _minGroundDotProduct, _minStairsDotProduct;

        private Vector3 _contactNormal, _steepNormal;
        private Vector3 _currentVelocity, _desiredVelocity;
        private Vector3 _upAxis, _rightAxis, _forwardAxis;

        private IInputService _inputService;
        private IGravityService _gravityService;

        public void Construct(IInputService inputService, IGravityService gravityService)
        {
            _inputService = inputService;
            _gravityService = gravityService;
        }

        private void Awake()
        {
            heroRigidbody.useGravity = false;
            OnValidate();
        }
        private void OnValidate()
        {
            _minGroundDotProduct = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);
            _minStairsDotProduct = Mathf.Cos(maxStairsAngle * Mathf.Deg2Rad);
        }
        private void Update()
        {
            _desiredJump |= _inputService.IsJumpPressed;

            Vector3 playerInput = Vector3.ClampMagnitude(_inputService.Axis, 1f);

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilone)
            {
                _rightAxis = ProjectDirectionOnPlane(_inputService.Right.ToPositive(), _upAxis);
                _forwardAxis = ProjectDirectionOnPlane(_inputService.Forward.ToPositive(), _upAxis);
            }
            else
            {
                _rightAxis = ProjectDirectionOnPlane(Vector3.right, _upAxis);
                _forwardAxis = ProjectDirectionOnPlane(Vector3.forward, _upAxis);
            }

            _desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        }
        private void FixedUpdate()
        {
            Vector3 gravity = _gravityService.Gravity(heroRigidbody.position, out _upAxis);

            _currentVelocity = heroRigidbody.velocity;

            AdjustVelocity();

            _stepsSinceLastGrounded += 1;
            _stepsSinceLastJump += 1;

            if (_desiredJump)
            {
                _desiredJump = false;
                Jump(gravity);
            }

            _currentVelocity += gravity * Time.deltaTime;

            if (IsOnGround() || CheckSteepContacts() || SnapToGround())
            {
                if (_stepsSinceLastGrounded > 1)
                    _jumpsCount = 0;
                _stepsSinceLastGrounded = 0;
                if (_groundContactCount > 1)
                    _contactNormal.Normalize();
            }
            else
                _contactNormal = _upAxis;

            heroRigidbody.velocity = _currentVelocity;

            ClearState();
        }
        private void OnCollisionEnter(Collision collision) =>
            EvaluateCollision(collision);
        private void OnCollisionStay(Collision collision) =>
            EvaluateCollision(collision);
        private void OnCollisionExit(Collision collision) =>
            _groundContactCount = 0;

        private void Jump(Vector3 gravity)
        {
            Vector3 jumpDirection;

            if (IsOnGround())
                jumpDirection = _contactNormal;
            else if (IsOnSteep())
            {
                jumpDirection = _steepNormal;
                _jumpsCount = 0;
            }
            else if (maxAirJumps > 0 && _jumpsCount <= maxAirJumps)
            {
                if (_jumpsCount == 0)
                    _jumpsCount = 1;
                jumpDirection = _contactNormal;
            }
            else
                return;

            _stepsSinceLastJump = 0;
            _jumpsCount += 1;

            float jumpSpeed = Mathf.Sqrt(2f * gravity.magnitude * jumpHeight);

            jumpDirection = (jumpDirection + _upAxis).normalized;

            float alignedSpeed = Vector3.Dot(_currentVelocity, jumpDirection);

            if (alignedSpeed > 0f)
                jumpSpeed = Mathf.Max(jumpSpeed - alignedSpeed, 0f);

            _currentVelocity += jumpDirection * jumpSpeed;
        }
        private void AdjustVelocity()
        {
            Vector3 xAxis = ProjectDirectionOnPlane(_rightAxis, _contactNormal);
            Vector3 zAxis = ProjectDirectionOnPlane(_forwardAxis, _contactNormal);

            float currentX = Vector3.Dot(_currentVelocity, xAxis);
            float currentZ = Vector3.Dot(_currentVelocity, zAxis);

            float maxSpeedChange = StateAcceleration() * Time.deltaTime;

            float newX = Mathf.MoveTowards(currentX, _desiredVelocity.x, maxSpeedChange);
            float newZ = Mathf.MoveTowards(currentZ, _desiredVelocity.z, maxSpeedChange);

            _currentVelocity += xAxis * (newX - currentX) + zAxis * (newZ - currentZ);
        }
        private Vector3 ProjectDirectionOnPlane(Vector3 direction, Vector3 normal)
        {
            return (direction - normal * Vector3.Dot(direction, normal)).normalized;
        }
        private float StateAcceleration() =>
            IsOnGround() ? maxGroundAcceleration : maxAirAcceleration;
        private bool SnapToGround()
        {
            if (_stepsSinceLastGrounded > 1 || _stepsSinceLastJump <= 2)
                return false;

            float speed = _currentVelocity.magnitude;


            if (speed > maxSnapSpeed)
                return false;
            if (!Physics.Raycast(heroRigidbody.position, -_upAxis, out RaycastHit hit, probeDistance, probeMask))
                return false;
            float upDot = Vector3.Dot(_upAxis, hit.normal);
            if (upDot < GetMinDot(hit.collider.gameObject.layer))
                return false;

            _groundContactCount = 1;
            _contactNormal = hit.normal;

            float dot = Vector3.Dot(_currentVelocity, hit.normal);
            if (dot > 0f)
                _currentVelocity = (_currentVelocity - hit.normal * dot).normalized * speed;
            return true;
        }
        private float GetMinDot(int layer)
        {
            return (stairsMask & (1 << layer)) == 0 ?
               _minGroundDotProduct : _minStairsDotProduct;
        }
        private bool CheckSteepContacts()
        {
            if (_steepContactCount > 1)
            {
                _steepNormal.Normalize();
                float upDot = Vector3.Dot(_upAxis, _steepNormal);
                if (upDot >= _minGroundDotProduct)
                {
                    _groundContactCount = 1;
                    _contactNormal = _steepNormal;
                    return true;
                }
            }
            return false;
        }
        private void EvaluateCollision(Collision collision)
        {
            int layer = collision.gameObject.layer;
            float minDot = GetMinDot(layer);
            for (int i = 0; i < collision.contactCount; i++)
            {
                Vector3 normal = collision.GetContact(i).normal;
                float upDot = Vector3.Dot(_upAxis, normal);
                if (upDot >= minDot)
                {
                    _groundContactCount += 1;
                    _contactNormal += normal;
                }
                else
                {
                    if (upDot > -0.01f)
                    {
                        _steepContactCount += 1;
                        _steepNormal += normal;
                    }
                }
            }
        }


        private void ClearState()
        {
            _groundContactCount = 0;
            _steepContactCount = 0;
            _contactNormal = Vector3.zero;
            _steepNormal = Vector3.zero;
        }
        private bool IsOnGround() => _groundContactCount > 0;
        private bool IsOnSteep() => _steepContactCount > 0;
    }
}
