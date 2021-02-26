using UnityEngine;

namespace CodeBase.Services.Physics
{
    public class PhysicsService : IPhysicsService
    {
        private Vector3 _velocity;
        private Rigidbody _rigidBody;
        private NormalDirection _normalSpaceDirection;

        public void SetRigidbody(Rigidbody rigidbody)
        {
            _rigidBody = rigidbody;
        }

        public float RBVelocityY
        {
            get => _velocity.y;
            set
            {
                _velocity.y = value;
                SyncVelocity();
            }
        }

        public Vector3 RBVelocity
        {
            get => _velocity;
            set 
            { 
                _velocity = value;
                SyncVelocity();
            }
        }
        public NormalDirection NormalSpaceDirection
        {
            get => _normalSpaceDirection;
            set => _normalSpaceDirection = value;
        }

        private void SyncVelocity() =>
            _rigidBody.velocity = ConvertToNormalSpace(_velocity);

        private Vector3 ConvertToNormalSpace(Vector3 currentVelocity)
        {
            switch (NormalSpaceDirection)
            {
                case NormalDirection.ZUp:
                    {
                        _velocity.y = currentVelocity.z;
                        _velocity.z = 0f;
                    }
                    break;
                case NormalDirection.XUp:
                    {
                        _velocity.y = currentVelocity.x;
                        _velocity.x = 0f;
                    }
                    break;
                case NormalDirection.ZReverseUp:
                    {
                        _velocity.y = -currentVelocity.z;
                        _velocity.z = 0f;
                    }
                    break;
                case NormalDirection.XReverseUp:
                    {
                        _velocity.y = -currentVelocity.x;
                        _velocity.x = 0f;
                    }
                    break;
            }
            return _velocity;
        }
    }
}