using UnityEngine;

namespace CodeBase.Services.Physics
{
    public class PhysicsService : IPhysicsService
    {
        private Vector3 _velocity;
        private Vector3 _jumpVelocity;

        private Rigidbody _rigidBody;
        private NormalDirection _normalSpaceDirection;

        public void SetRigidbody(Rigidbody rigidbody)
        {
            _rigidBody = rigidbody;
        }

        public float RBVelocityY
        {
            get => _jumpVelocity.y;
            set
            {
                _jumpVelocity.y = value;
                ConvertVelocity();
            }
        }

        public Vector3 RBVelocity
        {
            get => _velocity;
            set
            {
                _velocity = value;
                ConvertVelocity();
            }
        }

        public NormalDirection NormalSpaceDirection
        {
            get => _normalSpaceDirection;
            set => _normalSpaceDirection = value;
        }
        public Vector3 RBJumpVelocity
        {
            get => _jumpVelocity;
            set
            {
                _jumpVelocity = value;
                ConvertVelocity();
            }
        }

        private void ConvertVelocity()
        {
            Vector3 convertedVelocity = _velocity;

            switch (NormalSpaceDirection)
            {
                case NormalDirection.ZUp:
                    {
                        convertedVelocity.y = _velocity.z + _jumpVelocity.y;
                        convertedVelocity.z = 0f;
                    }
                    break;
                case NormalDirection.ZReverseUp:
                    {
                        convertedVelocity.y = -_velocity.z + _jumpVelocity.y;
                        convertedVelocity.z = 0f;
                    }
                    break;
                case NormalDirection.XUp:
                    {
                        convertedVelocity.y = _velocity.x + _jumpVelocity.y;
                        convertedVelocity.x = 0f;
                    }
                    break;
                case NormalDirection.XReverseUp:
                    {
                        convertedVelocity.y = -_velocity.x + _jumpVelocity.y;
                        convertedVelocity.x = 0f;
                    }
                    break;
            }

            _rigidBody.velocity = convertedVelocity + _jumpVelocity;
        }
    }
}