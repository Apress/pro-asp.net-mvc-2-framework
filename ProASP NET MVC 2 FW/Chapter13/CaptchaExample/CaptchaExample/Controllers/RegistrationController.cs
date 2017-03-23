using System.Web.Mvc;
using CaptchaExample.Helpers;

namespace CaptchaExample.Controllers
{
    public class RegistrationController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        public ActionResult SubmitRegistration(string myCaptcha, string attempt)
        {
            if (CaptchaHelper.VerifyAndExpireSolution(HttpContext, myCaptcha, attempt)) {
                // In a real app, actually register the user now
                return Content("Pass");
            } else {
                // Redisplay the view with an error message
                ModelState.AddModelError("attempt", "Incorrect - please try again");
                return View("Index");
            }
        }
    }
}
