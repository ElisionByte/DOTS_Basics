using System.Collections.Generic;

using CodeBase.Logic.GravitySources;

using UnityEngine;

namespace CodeBase.Services.Gravity
{
    public class GravityService : IGravityService
    {
        List<GravitySourse> _gravitySourses;

        public GravityService()
        {
            _gravitySourses = new List<GravitySourse>();
        }

        public List<GravitySourse> Sourses { get => _gravitySourses; set => _gravitySourses = value; }

        public Vector3 Gravity(Vector3 position) =>
            GetCurrentGravity(position);
        public Vector3 Gravity(Vector3 position, out Vector3 upAxis)
        {
            Vector3 gravity = GetCurrentGravity(position);
            upAxis = -gravity.normalized;
            return gravity;
        }
        public Vector3 UpAxis(Vector3 position)
        {
            return -GetCurrentGravity(position).normalized;
        }

        private Vector3 GetCurrentGravity(Vector3 position)
        {
            Vector3 resultGravity = Vector3.zero;
            foreach (GravitySourse sourse in _gravitySourses)
            {
                resultGravity += sourse.Gravity(position);
            }
            return resultGravity;
        }

        public void Register(GravitySourse sourse)
        {
            if(!_gravitySourses.Contains(sourse))
                _gravitySourses.Add(sourse);
        }
        public void Unregister(GravitySourse sourse)
        {
            if(_gravitySourses.Contains(sourse))
                _gravitySourses.Remove(sourse);
        }
    }
}