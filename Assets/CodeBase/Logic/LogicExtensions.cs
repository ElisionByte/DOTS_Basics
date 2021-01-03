using UnityEngine;

namespace CodeBase.Logic
{
    public static class CutingExtensions
    {
        public static GameObject AddCollider(this GameObject obj)
        {
            MeshCollider colider =  obj.AddComponent<MeshCollider>();
            colider.convex = true;
            return obj;
        }
        public static GameObject AddRigidbody(this GameObject obj)
        {
            obj.AddComponent<Rigidbody>();
            return obj;
        }
    }
}
