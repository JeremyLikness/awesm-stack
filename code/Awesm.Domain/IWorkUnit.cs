using System;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Awesm.Domain
{
    public interface IWorkUnit : IDisposable
    {
        IPrincipal Principal { get; set; }

        IQueryable<T> Collection<T>() where T: new();

        T GetContext<T>() where T : class;

        Task CommitAsync();
    }
}
