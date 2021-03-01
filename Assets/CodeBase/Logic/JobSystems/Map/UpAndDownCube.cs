using UnityEngine;

namespace CodeBase.Logic.JobSystems.Map
{
    public class UpAndDownCube : MonoBehaviour
    {
        public int YMax;
        public int YMin;
        public int Delta;

        public void Construct(int maxY, int minY, int delta)
        {
            YMax = maxY;
            YMin = minY;
            Delta = delta;
        }
    }
}