using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        public ViewResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnViewModel model, string returnUrl)
        {
            if (ModelState.IsValid) // No point trying authentication if model is invalid
                if (!FormsAuthentication.Authenticate(model.UserName, model.Password))
                    ModelState.AddModelError("", "Incorrect username or password");

            if (ModelState.IsValid) {
                // Grant cookie and redirect (to admin home if not otherwise specified)
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
            }
            else
                return View();
        }
    }
}
