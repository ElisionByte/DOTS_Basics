using UnityEngine.Jobs;

namespace Observers
{
    public interface IObserver
    {
        void UpdateTransform(TransformAccess transform);
        void UpdateSpawn();
    }
}
