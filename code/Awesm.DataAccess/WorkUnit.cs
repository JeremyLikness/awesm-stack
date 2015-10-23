using Awesm.Domain;
using System;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Linq;

namespace Awesm.DataAccess
{
    public class WorkUnit : IWorkUnit
    {
        private FoodContext _context;
        private bool _disposed;
        
        public WorkUnit()
        {
            _context = new FoodContext();
        }

        public WorkUnit(IPrincipal principal) : this()
        {
            Principal = principal;
        }

        public IPrincipal Principal { get; set; }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public T GetContext<T>() where T : class
        {
            return _context as T;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }

            _disposed = true;
        }

        public IQueryable<T> Collection<T>() where T : new()
        {
            if (typeof(T) == typeof(FoodDescription))
            {
                return (IQueryable<T>)_context.FoodDescriptions.AsQueryable();
            }

            throw new InvalidOperationException(string.Format("Cannot find a dataset for type {0}", typeof(T)));
        }
    }
}
