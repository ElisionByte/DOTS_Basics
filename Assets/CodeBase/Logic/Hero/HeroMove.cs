using CodeBase.Services.Physics;

using UnityEngine;

namespace CodeBase.Logic.Hero
{
    [RequireComponent(typeof(Rigidbody))]
    public class HeroMove : MonoBehaviour
    {
        [Range(1f, 100f)] public float maxSpeed;

        private Vector3 _desiredVelocity;

        private IInputService _inputService;
        private IPhysicsService _physicsService;

        private Vector3 _currentPosition;

        public void Construct(IInputService inputService,IPhysicsService physicsDisplaycementService)
        {
            _inputService = inputService;
            _physicsService = physicsDisplaycementService;
        }

        private void Update()
        {
            _desiredVelocity = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilone)
            {
                Vector3 playerInput = _inputService.Forward + _inputService.Right;
                _desiredVelocity = playerInput * maxSpeed;
            }
        }

        private void FixedUpdate()
        {
            _currentPosition = _physicsService.RBVelocity;

            _currentPosition.x = _desiredVelocity.x;
            _currentPosition.z = _desiredVelocity.z;

            _physicsService.RBVelocity = _currentPosition;
        }
    }
}