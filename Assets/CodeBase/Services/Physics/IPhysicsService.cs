using UnityEngine;

namespace CodeBase.Services.Physics
{
    public interface IPhysicsService : IService
    {
        float RBVelocityY { get; set; }

        NormalDirection NormalSpaceDirection { get; set; }

        Vector3 RBVelocity { get; set; }
        Vector3 RBJumpVelocity { get; set; }

        void SetRigidbody(Rigidbody rigidbody);
    }
}