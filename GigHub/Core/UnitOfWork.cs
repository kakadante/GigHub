using GigHub.Core.Repositories;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;

namespace GigHub.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGigRepository Gigs { get; private set; }
        public IAttendanceRepository Attendances { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRepository(context);
            Attendances = new AttendanceRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();   // You can call this METHOD Save() or SaveChanges()
        }
    }
}