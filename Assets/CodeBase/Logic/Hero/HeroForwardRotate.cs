using UnityEngine;

namespace CodeBase.Logic.Hero
{
    class HeroForwardRotate : MonoBehaviour
    {
        [Range(0.01f, 1f)] public float rotateTime;

        private float _turnVelocity;
        private IInputService _inputService;

        public void Construct(IInputService inputService) =>
            _inputService = inputService;

        private void Update()
        {
            RotateForvard();
        }

        private void RotateForvard()
        {
            if (_inputService.Axis.sqrMagnitude > Constants.Epsilone)
            {
                Vector3 direction = _inputService.Axis;
                float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnVelocity, rotateTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
        }
    }
}