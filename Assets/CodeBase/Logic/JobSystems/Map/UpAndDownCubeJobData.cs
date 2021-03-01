
using UnityEngine;

namespace CodeBase.Logic.JobSystems.Map
{
    public struct UpAndDownCubeJobData
    {
        private readonly int _maxY;
        private readonly int _minY;

        private int _delta;

        public UpAndDownCubeJobData(UpAndDownCube upAndDownCube)
        {
            _maxY = upAndDownCube.YMax;
            _minY = upAndDownCube.YMin;
            _delta = upAndDownCube.Delta;
        }

        public Vector3 UpdateState(float yValue)
        {
            Vector3 yPosition = Vector3.zero;

            if (yValue > _maxY || yValue < _minY)
                _delta *= -1;

            yValue += _delta;

            yPosition.y = yValue;

            return yPosition;
        }
    }
}