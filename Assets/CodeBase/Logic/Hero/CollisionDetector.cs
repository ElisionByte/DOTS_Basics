using CodeBase.Services.Physics;

using UnityEngine;

namespace CodeBase.Logic.Hero
{
    public class CollisionDetector : MonoBehaviour
    {
        [Range(0f, 90f)] public float maxGroundAngle = 25f;
        [Range(90f, 180f)] public float maxClimbAngle = 90f;

        public bool IsGrounded => _groundNormalsCount > 0;
        public bool IsClimbing => _climbNormalsCount > 0;
        public Vector3 ContactNormal => _contactNormal;

        private int _groundNormalsCount, _climbNormalsCount;
        private float _minGroundDotProduct;
        private float _minClimbDotProduct;
        private Vector3 _contactNormal;

        private IPhysicsService _physicsService;

        public void Construct(IPhysicsService physicsService)
        {
            _physicsService = physicsService;
        }

        private void Awake()
        {
            _minGroundDotProduct = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);
            _minClimbDotProduct = Mathf.Cos(maxClimbAngle * Mathf.Deg2Rad);
        }
        private void FixedUpdate() =>
            ClearState();
        private void OnCollisionEnter(Collision collision) =>
           EvaluateCollision(collision);
        private void OnCollisionStay(Collision collision) =>
            EvaluateCollision(collision);
        private void OnCollisionExit(Collision collision) =>
            _groundNormalsCount = 0;

        private void EvaluateCollision(Collision collision)
        {
            Debug.Log(collision.gameObject.name);

            for (int i = 0; i < collision.contactCount; i++)
            {
                Vector3 normal = collision.GetContact(i).normal;

                float upDot = Vector3.Dot(Vector3.up, normal);

                if (upDot >= _minGroundDotProduct)
                {
                    _contactNormal = Vector3.zero;
                    _groundNormalsCount += 1;
                    _contactNormal += normal;
                }
                else if (upDot > _minClimbDotProduct)
                {
                    _contactNormal = Vector3.zero;
                    _climbNormalsCount++;

                    if (normal.normalized.z >= Constants.Epsilone || normal.normalized.z <= Constants.Epsilone * -1)
                        _physicsService.NormalSpaceDirection = NormalDirection.ZUp;
                    if (normal.normalized.x >= Constants.Epsilone || normal.normalized.x <= Constants.Epsilone * -1)
                        _physicsService.NormalSpaceDirection = NormalDirection.XUp;

                    _contactNormal += normal;
                }
            }
        }
        private void ClearState()
        {
            _groundNormalsCount = 0;
            _climbNormalsCount = 0;
            _physicsService.NormalSpaceDirection = NormalDirection.YUp;
        }
    }
}