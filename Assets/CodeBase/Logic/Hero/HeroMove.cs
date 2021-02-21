using CodeBase.Services.Physics;

using UnityEngine;

namespace CodeBase.Logic.Hero
{
    [RequireComponent(typeof(Rigidbody))]
    public class HeroMove : MonoBehaviour
    {
        [Range(1f, 100f)] public float maxSpeed;

        private Vector3 _currentVelocity, _desiredVelocity;

        private IInputService _inputService;
        private IPhysicsService _physicsDisplaycementService;

        public void Construct(IInputService inputService,IPhysicsService physicsDisplaycementService)
        {
            _inputService = inputService;
            _physicsDisplaycementService = physicsDisplaycementService;
        }

        private void Update()
        {
            _desiredVelocity = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilone)
            {
                Vector3 playerInput = new Vector3(_inputService.Axis.x, 0f, _inputService.Axis.y);
                _desiredVelocity = playerInput * maxSpeed;
            }
        }

        private void FixedUpdate()
        {
            _currentVelocity = _physicsDisplaycementService.Velocity;
            _currentVelocity.x = _desiredVelocity.x;
            _currentVelocity.z = _desiredVelocity.z;
            _physicsDisplaycementService.Velocity = _currentVelocity;
        }
    }
}