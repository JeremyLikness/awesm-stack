using System.Web.Mvc;

namespace Awesm.Filters
{
    public class AngularRequestFilterAttribute : ActionFilterAttribute
    {
        private const string AngularRequest = "Angular-Request";
        private const string Value = "awesm";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;

            if (request.Headers[AngularRequest] != null && request.Headers[AngularRequest] == Value)
            {
                base.OnActionExecuting(filterContext);
                return;
            }
            filterContext.Result = new HttpUnauthorizedResult("The request must be made from within the application.");
        }
    }
}