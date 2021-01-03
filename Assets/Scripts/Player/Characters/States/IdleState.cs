using UnityEngine;

namespace Player.Characters.States
{
    public class IdleState : State
    {
        public IdleState(Character character) : base(character)
        { }

        public override void OnStateEnter()
        {
        }

        public override void OnStateExit()
        {
        }

        public override void Tick()
        {

            Stand();
            if (base.character.MoveDirection.magnitude > 0f)
                base.character.SetState(new WalkState(base.character));
        }

        private void Stand()
        {
            float _horizintal = Input.GetAxis("Horizontal");
            float _vertical = Input.GetAxis("Vertical");

            base.character.MoveDirection = new Vector3(_horizintal, base.character.MoveDirection.y, _vertical) * base.character.Speed * Time.fixedDeltaTime;
            base.character.JumpCheck();
        }
    }
}
