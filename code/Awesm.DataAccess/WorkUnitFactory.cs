using Awesm.Domain;
using System.Security.Principal;

namespace Awesm.DataAccess
{
    public class WorkUnitFactory : IWorkUnitFactory
    {
        public IWorkUnit CreateWorkUnit(IPrincipal principal)
        {
            return new WorkUnit(principal);
        }
    }
}
