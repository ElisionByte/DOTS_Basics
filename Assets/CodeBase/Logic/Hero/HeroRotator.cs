using System;

using CodeBase.Logic.Hero.Animation;

using UnityEngine;

namespace CodeBase.Logic.Hero
{
    public class HeroRotator : MonoBehaviour
    {
        [Range(0.01f, 1f)] public float rotateTime;

        public new Camera camera;
        public Transform rootTransform;
        public Transform meshTransform;
        public CollisionDetector CollisionDetector;

        public AnimatorState AnimatorState;

        private float _turnVelocity;
        private float _lastAngle;
        private IInputService _inputService;

        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Update()
        {
            if (!CollisionDetector.IsClimbing)
            {
                MeshRotate();
            }
            else
            {
                StateChange();
            }
        }
        private void LateUpdate()
        {
            rootTransform.rotation = Quaternion.Euler(UpVector());
        }

        public void StateChange()
        {
            switch (AnimatorState)
            {
                case AnimatorState.WallRun:
                    {
                        meshTransform.rotation = Quaternion.Euler(CollisionDetector.ContactNormal);
                    }
                    break;
                default:
                    break;
            }
        }

        private void MeshRotate()
        {
            if (_inputService.Axis.sqrMagnitude > Constants.Epsilone)
            {
                Vector2 _direction = _inputService.Axis;

                float targetAngle = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg;

                _lastAngle = Mathf.SmoothDampAngle(meshTransform.transform.eulerAngles.y, rootTransform.rotation.eulerAngles.y + targetAngle, ref _turnVelocity, rotateTime);
            }

            meshTransform.rotation = Quaternion.Euler(0, _lastAngle, 0);
        }
        private Vector3 UpVector()
        {
            return new Vector3(0, camera.transform.rotation.eulerAngles.y, 0);
        }
    }
}