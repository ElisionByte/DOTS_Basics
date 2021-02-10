using CodeBase.Services.Inputs;

using UnityEngine;

namespace CodeBase.Services.Inputs
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis => StandaloneInputAxis();
    }
}
