using CodeBase.Logic.GravitySources;
using CodeBase.Services.Gravity;

using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class MapFactory : IMapFactory
    {
        private readonly IGravityService _gravityService;

        public MapFactory(IGravityService gravityService)
        {
            _gravityService = gravityService;
        }

        public void InitialiseMapComponents(GravitySourse[] sourses)
        {
            foreach (GravitySourse sourse in sourses)
            {
                sourse.Construct(_gravityService);
            }
        }
    }
}