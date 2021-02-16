using System.Collections.Generic;

using CodeBase.Logic.GravitySources;

using UnityEngine;

namespace CodeBase.Services.Gravity
{
    public interface IGravityService : IService
    {
        List<GravitySourse> Sourses { get; set; }

        Vector3 Gravity(Vector3 position);
        Vector3 Gravity(Vector3 position, out Vector3 upAxis);

        Vector3 UpAxis(Vector3 position);

        void Register(GravitySourse sourse);
        void Unregister(GravitySourse sourse);
    }
}