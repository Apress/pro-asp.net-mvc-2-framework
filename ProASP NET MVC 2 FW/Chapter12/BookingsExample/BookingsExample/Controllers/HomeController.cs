using System.Web.Mvc;

namespace BookingsExample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("MakeBooking", "Booking");
        }
    }
}