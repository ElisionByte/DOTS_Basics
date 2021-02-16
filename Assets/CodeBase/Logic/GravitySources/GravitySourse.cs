using CodeBase.Services.Gravity;

using UnityEngine;

namespace CodeBase.Logic.GravitySources
{
    public class GravitySourse : MonoBehaviour
    {
        private IGravityService _gravityService;

        public void Construct(IGravityService gravityService)
        {
            _gravityService = gravityService;
            _gravityService.Register(this);
        }

        public virtual Vector3 Gravity(Vector3 position) =>
            Physics.gravity;

        private void OnEnable() => 
            _gravityService?.Register(this);

        private void OnDisable() =>
            _gravityService?.Unregister(this);
    }
}