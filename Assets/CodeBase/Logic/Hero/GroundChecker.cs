using System;

using UnityEngine;

namespace CodeBase.Logic.Hero
{
    public class GroundChecker : MonoBehaviour
    {
        private int _groundNormalsCount;

        public bool IsGrounded => _groundNormalsCount > 0;

        private void FixedUpdate() => 
            ClearState();
        private void OnCollisionEnter(Collision collision) =>
           EvaluateCollision(collision);
        private void OnCollisionStay(Collision collision) =>
            EvaluateCollision(collision);
        private void OnCollisionExit(Collision collision) =>
            _groundNormalsCount = 0;

        private void ClearState() => 
            _groundNormalsCount = 0;

        private void EvaluateCollision(Collision collision)
        {
            int layer = collision.gameObject.layer;
            for (int i = 0; i < collision.contactCount; i++)
            {
                Vector3 normal = collision.GetContact(i).normal;
                if (normal.y > 0)
                {
                    _groundNormalsCount += 1;
                }
            }
        }
    }
}