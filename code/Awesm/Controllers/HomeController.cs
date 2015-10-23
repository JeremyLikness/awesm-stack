using Awesm.Filters;
using System.Web.Mvc;

namespace Awesm.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [AngularRequestFilter]
        public ActionResult FoodList()
        {
            return PartialView();
        }
    }
}