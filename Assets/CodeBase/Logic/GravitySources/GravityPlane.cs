using UnityEngine;

namespace CodeBase.Logic.GravitySources
{
    public class GravityPlane : GravitySourse
    {
        public float gravity = 9.81f;

        public Color gizmosColor;
        [Range(0f, 100f)] public float _gizmosWidth = 1f;
        [Range(0f, 100f)] public float _gizmosHeight = 1f;
        [Range(0f, 100f)] public float range = 1f;

        private void OnDrawGizmos()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Vector3 planeSize = new Vector3(_gizmosWidth, 0f, _gizmosHeight);
            Gizmos.color = gizmosColor;
            Gizmos.DrawWireCube(Vector3.zero, planeSize);

            if (range > 0f)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawWireCube(new Vector3(0f, range, 0f), planeSize);
            }
        }

        public override Vector3 Gravity(Vector3 position)
        {
            Vector3 up = transform.up;
            float distance = Vector3.Dot(up, position - transform.position);
            if (distance > range)
            {
                return Vector3.zero;
            }
            float g = -gravity;
            if (distance > 0f)
            {
                g *= 1f - distance / range;
            }
            return g * up;
        }
    }
}