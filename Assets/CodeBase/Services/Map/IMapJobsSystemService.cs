using System.Collections.Generic;

using CodeBase.Logic.JobSystems.Map;

namespace CodeBase.Services.Map
{
    public interface IMapJobsSystemService : IService
    {
        void CreateJob(List<UpAndDownCube> cubes);
        void ScheduleJob();
        void CompleteJob();
        void DisposeJob();
    }
}