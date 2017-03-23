using System.Web;
using System.Web.Mvc;

namespace UnitTestingControllers.Controllers
{
    public class SimpleController : Controller
    {
        // There's no view for this action. You'll need to add one if you want to run this code.
        // However, we don't need any view to write and run unit tests.
        public ViewResult Homepage()
        {
            if (IncomingHasVisitedBeforeCookie == null)
            {
                ViewData["IsFirstVisit"] = true;
                // Set the cookie so we'll remember the visitor next time
                OutgoingHasVisitedBeforeCookie = new HttpCookie("HasVisitedBefore", "True");
            }
            else
                ViewData["IsFirstVisit"] = false;
            return View();
        }

        public virtual HttpCookie IncomingHasVisitedBeforeCookie
        {
            get { return Request.Cookies["HasVisitedBefore"]; }
        }

        public virtual HttpCookie OutgoingHasVisitedBeforeCookie
        {
            get { return Response.Cookies["HasVisitedBefore"]; }
            set
            {
                Response.Cookies.Remove("HasVisitedBefore");
                Response.Cookies.Add(value);
            }
        }
    }
}
