using UnityEngine.Jobs;
using Unity.Collections;
using UnityEngine;
using System.Collections.Generic;
using Unity.Jobs;
using CodeBase.Services.Map;

namespace CodeBase.Logic.JobSystems.Map
{
    public class MapJobsSystemService : IMapJobsSystemService
    {
        private NativeArray<UpAndDownCubeJobData> _upAndDownCubeJobData;
        private TransformAccessArray _transformAccessArray;

        private MapUpAndDownCubeJob _mapUpAndDownCubeJob;
        private JobHandle _jobHandle;

        public void CreateJob(List<UpAndDownCube> cubes)
        {
            var cubesData = new UpAndDownCubeJobData[cubes.Count];
            var transformData = new Transform[cubes.Count];

            for (int i = 0; i < cubes.Count; i++)
            {
                cubesData[i] = new UpAndDownCubeJobData(cubes[i]);
                transformData[i] = cubes[i].transform;
            }

            _transformAccessArray = new TransformAccessArray(transformData);
            _upAndDownCubeJobData = new NativeArray<UpAndDownCubeJobData>(cubesData, Allocator.TempJob);

            _mapUpAndDownCubeJob = new MapUpAndDownCubeJob { JobData = _upAndDownCubeJobData };
        }

        public void ScheduleJob()
        {
            _jobHandle = _mapUpAndDownCubeJob.Schedule(_transformAccessArray);
        }

        public void CompleteJob() =>
            _jobHandle.Complete();
        public void DisposeJob()
        {
            _upAndDownCubeJobData.Dispose();
            _transformAccessArray.Dispose();
        }
    }
}