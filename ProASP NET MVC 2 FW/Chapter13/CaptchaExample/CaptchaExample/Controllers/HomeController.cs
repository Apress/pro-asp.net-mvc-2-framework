using System.Web.Mvc;

namespace CaptchaExample.Controllers
{
    public class HomeController : Controller
    {
        public RedirectToRouteResult Index()
        {
            return RedirectToAction("Index", "Registration");
        }
    }
}