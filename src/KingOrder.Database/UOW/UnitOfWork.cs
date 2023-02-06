using KingOrder.Database.Contexts;
using KingOrder.Domain.Interfaces;

namespace KingOrder.Database.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly KingOrderContext _context;

        private bool _disposed;

        public UnitOfWork(KingOrderContext context)
        {
            _context = context;
        }

        public void BeginTransaction()
        {
            _disposed = false;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
