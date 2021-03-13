using UnityEngine;

namespace CodeBase.Services.Inputs
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis => StandaloneInputAxis();
        public override float RightValue => Axis.x;
        public override float ForwardValue => Axis.y;
    }
}