using UnityEngine;

namespace CodeBase.Services.Physics
{
    public interface IPhysicsService : IService
    {
        Vector3 Velocity { get; set; }
        void SetRigidbody(Rigidbody rigidbody);
    }

    public class PhysicsDisplaycementService : IPhysicsService
    {
        private Rigidbody _rigidBody;
        
        public void SetRigidbody(Rigidbody rigidbody)
        {
            _rigidBody = rigidbody;
        }

        public Vector3 Velocity 
        {
            get => _rigidBody.velocity;
            set => _rigidBody.velocity = value; 
        }
    }
}