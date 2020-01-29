using GigHub.Core.Repositories;

namespace GigHub.Persistence
{
    public interface IUnitOfWork
    {
        IAttendanceRepository Attendances { get; }
        IGigRepository Gigs { get; }

        void Complete();
    }
}