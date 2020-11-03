using UnityEngine;

namespace Assets.Scripts.Hero
{
    class Robot : Hero, IHero
    {
        private float _horizintal = 0f;
        private float _vertical = 0f;

        private float _turnSmothVelocity = 0f;

        Vector3 _direction = Vector3.zero;
        Vector3 _moveDirection = Vector3.zero;

        private bool IsPlayerJumped => Controller.isGrounded && Input.GetKey(KeyCode.Space);

        private void FixedUpdate()
        {
            Walk();
            Rotate();
        }

        public void JumpCheck()
        {
            if (IsPlayerJumped)
                _moveDirection.y = JumpSpeed;
            else if (Controller.isGrounded)
                _moveDirection.y = 0f;
            else
                _moveDirection.y -= GRAVITY * Time.fixedDeltaTime;
        }

        public void Run()
        {

        }

        public void Walk()
        {
            _horizintal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Vertical");

            _direction = new Vector3(_horizintal, 0f, _vertical) * Speed * Time.fixedDeltaTime;

            _moveDirection = new Vector3(_direction.x, _moveDirection.y, _direction.z);

            JumpCheck();

            Animator.SetFloat("Speed", _direction.magnitude);
            Controller.Move(_moveDirection);

        }

        public void Rotate()
        {
            if (_direction.magnitude > 0)
            {
                float targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmothVelocity, TurnSmothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
        }

        public void ToCheckPoint()
        {
            gameObject.SetActive(false);
            transform.position = new Vector3(5f, 0f, 7f);
            gameObject.SetActive(true);
        }
    }
}
