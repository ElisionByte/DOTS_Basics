using System.Collections.Generic;

using CodeBase.Logic.JobSystems.Map;
using CodeBase.Services.Map;

using UnityEngine;

namespace CodeBase.Logic.Map
{
    public class MapObserverNotificator : MonoBehaviour
    {
        private IMapJobsSystemService _mapJobsSystemService;

        private readonly List<UpAndDownCube> _upAndDownCubes;

        public void Construct(IMapJobsSystemService mapJobsSystemService, List<UpAndDownCube> upAndDownCubes)
        {
            _mapJobsSystemService = mapJobsSystemService;
            _mapJobsSystemService.CreateJob(upAndDownCubes);
        }

        private void Update() =>
            _mapJobsSystemService.ScheduleJob();

        private void LateUpdate() => 
            _mapJobsSystemService.CompleteJob();

        private void OnDestroy() => 
            _mapJobsSystemService.DisposeJob();
    }
}