using UnityEngine;

namespace CodeBase.Services.Inputs
{
    public abstract class InputService : IInputService
    {
        private const string _horizontal = "Horizontal";
        private const string _vertical = "Vertical";
        private const string _jumpButtonName = "Jump";

        public abstract Vector2 Axis { get; }
        public abstract float RightValue { get; }
        public abstract float ForwardValue { get; }

        public bool IsJumpPressed => Input.GetButtonDown(_jumpButtonName);

        protected Vector2 StandaloneInputAxis() =>
            new Vector2(Input.GetAxis(_horizontal), Input.GetAxis(_vertical));
    }
}