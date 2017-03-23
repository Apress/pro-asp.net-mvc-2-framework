using System.Web.Mvc;
using Microsoft.Web.Mvc;
using WizardExample.Models;

namespace WizardExample.Controllers
{
    [ValidateOnlyIncomingValues]
    public class RegistrationController : Controller
    {
        private RegistrationData regData;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var serialized = Request.Form["regData"];
            if (serialized != null) // Form was posted containing serialized data
            { 
                regData = (RegistrationData)new MvcSerializer().Deserialize(serialized);
                TryUpdateModel(regData);
            }
            else
                regData = (RegistrationData)TempData["regData"] ?? new RegistrationData();
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (filterContext.Result is RedirectToRouteResult)
                TempData["regData"] = regData;
        }


        public ActionResult BasicDetails(string nextButton)
        {
            if ((nextButton != null) && ModelState.IsValid)
                return RedirectToAction("ExtraDetails");
            return View(regData);
        }

        public ActionResult ExtraDetails(string backButton, string nextButton)
        {
            if (backButton != null)
                return RedirectToAction("BasicDetails");
            else if ((nextButton != null) && ModelState.IsValid)
                return RedirectToAction("Confirm");
            else
                return View(regData);
        }


        public ActionResult Confirm(string backButton, string nextButton)
        {
            if (backButton != null)
                return RedirectToAction("ExtraDetails");
            else if (nextButton != null)
                return RedirectToAction("Complete");
            else
                return View(regData);
        }

        public ActionResult Complete()
        {
            // Todo: Save regData to database; render a "completed" view
            return Content("OK, we're done");
        }
    }
}