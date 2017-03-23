using System.Web.Mvc;

namespace UnitTestingRoutes.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Content("This is the homepage");
        }
    }
}
