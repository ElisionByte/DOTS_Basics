using UnityEngine;

namespace Player.Characters.States
{
    public class WalkState : State
    {


        public WalkState(Character character) : base(character)
        { }

        public override void OnStateEnter()
        {
            Debug.Log("WalkEnter");
        }
        public override void OnStateExit()
        {
            Debug.Log("WalkExit");
        }
        public override void Tick()
        {

            Walk();
            if (base.character.MoveDirection.magnitude == 0)
                base.character.SetState(new IdleState(base.character));
        }

        private void Walk()
        {
            float _horizintal = Input.GetAxis("Horizontal");
            float _vertical = Input.GetAxis("Vertical");

            base.character.MoveDirection = new Vector3(_horizintal, base.character.MoveDirection.y, _vertical) * base.character.Speed * Time.fixedDeltaTime;

            base.character.RotationDirection = new Vector2(base.character.MoveDirection.x,base.character.MoveDirection.z);

            base.character.Animator.SetFloat("Speed", Mathf.Abs(_horizintal) + Mathf.Abs(_vertical));
            base.character.JumpCheck();
            base.character.RotateCheck();
            base.character.Controller.Move(base.character.MoveDirection);
        }
    }
}