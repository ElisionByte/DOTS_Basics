using UnityEngine;

namespace CodeBase.Logic.JobSystems.Map
{
    public class UpAndDownCube : MonoBehaviour
    {
        public float YMax;
        public float YMin;
        public float Delta;
        public float Speed;

        public void Construct(float distance, float speed, float delta)
        {
            YMax = transform.position.y + distance;
            YMin = transform.position.y - distance;
            Delta = delta;
            Speed = speed;
        }
    }
}