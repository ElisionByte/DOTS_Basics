using Observers;
using Unity.Collections;
using UnityEngine.Jobs;

namespace Jobs
{
    public struct SubjectTransformJob : IJobParallelForTransform
    {
        public NativeArray<ObserverData> Data;

        public void Execute(int index, TransformAccess transform)
        {
            var data = Data[index];
            data.UpdateTransformData(transform);
            Data[index] = data;
        }
    }
}
