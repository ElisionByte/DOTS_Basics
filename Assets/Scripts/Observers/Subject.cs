using Jobs;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace Observers
{
    public class Subject : ISubject
    {
        private SubjectTransformJob _job;
        private JobHandle _jobHandle;
        private NativeArray<ObserverData> _subjectDataArray;
        private TransformAccessArray _transformAccessArray;

        public void Attach(List<Observer> observers)
        {
            var subjectData = new ObserverData[observers.Count];
            var transfromData = new Transform[observers.Count];

            for (int i = 0; i < subjectData.Length; i++)
            {
                subjectData[i] = new ObserverData(observers[i]);
                transfromData[i] = observers[i].transform;
            }

            _transformAccessArray = new TransformAccessArray(transfromData);
            _subjectDataArray = new NativeArray<ObserverData>(subjectData, Allocator.Persistent);

            _job = new SubjectTransformJob { Data = _subjectDataArray };
        }
        public void Detach()
        {
            _subjectDataArray.Dispose();
            _transformAccessArray.Dispose();
        }
        public void Notify()
        {
            _jobHandle = _job.Schedule(_transformAccessArray);
            JobHandle.ScheduleBatchedJobs();
            _jobHandle.Complete();
        }
    }
}