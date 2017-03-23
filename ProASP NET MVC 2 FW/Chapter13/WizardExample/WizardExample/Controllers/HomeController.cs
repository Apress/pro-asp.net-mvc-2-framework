using System.Web.Mvc;

namespace WizardExample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("BasicDetails", "Registration");
        }
    }
}