using CodeBase.Logic.Weapon;

using Player.Characters.States;

using UnityEngine;

namespace Player.Characters
{
    class Robot : Character
    {
        public Katana weapon;
        public BoxCollider boxCollider;
        public int layerID;
        private float _turnSmothVelocity = 0f;

        private bool IsPlayerJumped => Controller.isGrounded && Input.GetKey(KeyCode.Space);

        private void Start()
        {
            SetState(new IdleState(this));
            //weapon.Setup(boxCollider, layerID);
        }

        private void FixedUpdate()
        {
            currentState.Tick();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                weapon.Atack();
            }
        }

        public override void JumpCheck()
        {
            if (IsPlayerJumped)
                MoveDirection.y += jumpSpeed;
            else if (Controller.isGrounded)
                MoveDirection.y = 0f;
            else
                MoveDirection.y -= gravity * Time.fixedDeltaTime;
        }
        public override void RotateCheck()
        {
            if (RotationDirection.magnitude > 0)
            {
                float targetAngle = Mathf.Atan2(RotationDirection.x, RotationDirection.y) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmothVelocity, turnSmothTime);
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