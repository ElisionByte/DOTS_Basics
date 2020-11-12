using Assets.Scripts;
using UnityEngine;

namespace ObjectPool.Pools
{
    public sealed class SphereBulletPool : ObjectPool<SphereBullet>
    {
        public Transform spawnTransform;
        public float instanceLifeTime;
        public void SpawnSphereBullet()
        {
            SphereBullet sphereBullet = base.Get();
            sphereBullet.SetPosition(spawnTransform.position);
            sphereBullet.Show();
        }
    }
}