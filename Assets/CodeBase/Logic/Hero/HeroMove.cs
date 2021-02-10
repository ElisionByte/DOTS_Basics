using UnityEngine;

namespace CodeBase.Logic.Hero
{
    [RequireComponent(typeof(Rigidbody))]
    public class HeroMove : MonoBehaviour
    {
        public Rigidbody heroRigidbody;
        public GroundCheker groundCheker;

        [Range(1f, 30f)] public float maxSpeed;
        [Range(1f, 30f)] public float maxGroundAcceleration;
        [Range(1f, 30f)] public float maxAirAcceleration;

        private Vector3 _desiredVelocity;
        private Vector3 _currentVelocity;
        private IInputService _inputService;

        public void Construct(IInputService inputService) =>
            _inputService = inputService;

        private void Update()
        {
            Vector2 playerInput = Vector2.ClampMagnitude(_inputService.Axis, 1f);
            _desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        }
        private void FixedUpdate()
        {
            _currentVelocity = heroRigidbody.velocity;
            AdjustVelocity();
            heroRigidbody.velocity = _currentVelocity;
        }

        private void AdjustVelocity()
        {
            Vector3 xAxis = ProjectOnContactPlane(Vector3.right).normalized;
            Vector3 zAxis = ProjectOnContactPlane(Vector3.forward).normalized;

            float currentX = Vector3.Dot(_currentVelocity, xAxis);
            float currentZ = Vector3.Dot(_currentVelocity, zAxis);

            float maxSpeedChange = StateAcceleration() * Time.deltaTime;

            float newX = Mathf.MoveTowards(currentX, _desiredVelocity.x, maxSpeedChange);
            float newZ = Mathf.MoveTowards(currentZ, _desiredVelocity.z, maxSpeedChange);

            _currentVelocity += xAxis * (newX - currentX) + zAxis * (newZ - currentZ);
        }
        private Vector3 ProjectOnContactPlane(Vector3 vector) =>
            vector - groundCheker.ContactNormal * Vector3.Dot(vector, groundCheker.ContactNormal);
        private float StateAcceleration() =>
            groundCheker.OnGround ? maxGroundAcceleration : maxAirAcceleration;
    }
}