using UnityEngine;

namespace CodeBase.Logic.JobSystems.Map
{
    public struct UpAndDownCubeJobData
    {
        private readonly float _maxY;
        private readonly float _minY;

        private float _speed;
        private float _delta;

        public UpAndDownCubeJobData(UpAndDownCube upAndDownCube)
        {
            _maxY = upAndDownCube.YMax;
            _minY = upAndDownCube.YMin;
            _delta = upAndDownCube.Delta;
            _speed = upAndDownCube.Speed;
        }

        public Vector3 UpdateState(Vector3 transform)
        {
            Vector3 resultPosition = transform;

            if (resultPosition.y > _maxY)
                _speed = _speed > 0 ? _speed * -1 : _speed;
            else if (resultPosition.y < _minY)
                _speed = _speed < 0 ? _speed * -1 : _speed;

            resultPosition.y += _speed;

            return Vector3.Lerp(transform, resultPosition, Mathf.Abs(_speed) * _delta);
        }
    }
}