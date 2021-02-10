using UnityEngine;

namespace CodeBase.Logic.Hero
{
    public class GroundCheker : MonoBehaviour
    {
        [Range(0f, 90f)] public float maxGroundAngle = 25f;

        public bool OnGround => _groundContactCount > 0;
        public Vector3 ContactNormal { get => _contactNormal; set => _contactNormal = value; }
        public float MinGroundDotProduct => _minGroundDotProduct;
        public int GroundContactCount => _groundContactCount;

        private int _groundContactCount;
        private float _minGroundDotProduct;
        private Vector3 _contactNormal;

        private void Awake() =>
            _minGroundDotProduct = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);
        private void FixedUpdate() =>
            ClearState();
        private void OnCollisionEnter(Collision collision) =>
            EvaluateCollision(collision);
        private void OnCollisionStay(Collision collision) =>
            EvaluateCollision(collision);
        private void OnCollisionExit(Collision collision) =>
            _groundContactCount = 0;

        private void EvaluateCollision(Collision collision)
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                Vector3 normal = collision.GetContact(i).normal;
                if (normal.y >= +_minGroundDotProduct)
                {
                    _groundContactCount += 1;
                    _contactNormal += normal;
                }
            }
        }
        private void ClearState()
        {
            _groundContactCount = 0;
            _contactNormal = Vector3.zero;
        }
    }
}