using UnityEngine;

namespace CodeBase.Services.Physics
{
    public enum NormalDirection
    {
        YUp,
        ZUp,
        XUp
    }

    public interface IPhysicsService : IService
    {
        float RBVelocityX { get; set; }
        float RBVelocityY { get; set; }
        float RBVelocityZ { get; set; }

        NormalDirection NormalSpaceDirection { get; set; }

        Vector3 RBVelocity { get; set; }

        void SetRigidbody(Rigidbody rigidbody);
    }

    public class PhysicsDisplaycementService : IPhysicsService
    {
        private Vector3 _velocity;
        private Rigidbody _rigidBody;
        private NormalDirection _normalSpaceDirection;

        public void SetRigidbody(Rigidbody rigidbody)
        {
            _rigidBody = rigidbody;
        }

        public float RBVelocityX
        {
            get => _velocity.x;
            set
            {
                Debug.Log("F : " + _velocity.x);
                _velocity.x = value;
                SyncVelocity();
            }
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
        public float RBVelocityZ
        {
            get => _velocity.z;
            set
            {
                _velocity.z = value;
                SyncVelocity();
            }
        }

        public Vector3 RBVelocity
        {
            get => _velocity;
            set { _velocity = value; SyncVelocity(); }
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
                        _velocity.y = -currentVelocity.x;
                        _velocity.x = 0f;
                    }
                    break;
            }
            return _velocity;
        }
    }
}