using UnityEngine;

namespace CodeBase.Services.Inputs
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis => StandaloneInputAxis();
        public override Vector3 Right => new Vector3(Axis.x, 0f, 0f);
        public override Vector3 Forward => new Vector3(0f, 0f, Axis.y);
    }
}