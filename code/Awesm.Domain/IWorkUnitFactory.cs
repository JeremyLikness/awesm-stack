using System.Security.Principal;

namespace Awesm.Domain
{
    public interface IWorkUnitFactory
    {
        IWorkUnit CreateWorkUnit(IPrincipal principal);
    }
}
