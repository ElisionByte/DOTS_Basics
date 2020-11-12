using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace Observer
{
    public class Subject : ISubject
    {
        private List<Observer> _observers = new List<Observer>();

        private SubjectJob _job;
        private JobHandle _jobHandle;
        private NativeArray<ObserverData> _subjectDataArray;
        private TransformAccessArray _transformAccessArray;

        public void Attach(List<Observer> observers)
        {
            _observers = observers;

            var subjectData = new ObserverData[_observers.Count];
            var transfromData = new Transform[_observers.Count];

            for (int i = 0; i < subjectData.Length; i++)
            {
                subjectData[i] = new ObserverData(_observers[i]);
                transfromData[i] = _observers[i].transform;
            }

            _transformAccessArray = new TransformAccessArray(transfromData);
            _subjectDataArray = new NativeArray<ObserverData>(subjectData, Allocator.Persistent);

            _job = new SubjectJob { Data = _subjectDataArray };
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
    public struct SubjectJob : IJobParallelForTransform
    {
        public NativeArray<ObserverData> Data;

        public void Execute(int index, TransformAccess transform)
        {
            var data = Data[index];
            data.UpdateData(transform);
            Data[index] = data;
        }
    }
}