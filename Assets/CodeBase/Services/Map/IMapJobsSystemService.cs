namespace CodeBase.Services.Map
{
    public interface IMapJobsSystemService : IService
    {
        void ScheduleJob();
        void CompleteJob();
        void DisposeJob();
    }
}