using System.Web.Mvc;

namespace jQueryStockQuotesExample.Controllers
{
    public class HomeController : Controller
    {
        public RedirectToRouteResult Index()
        {
            return RedirectToAction("Index", "Stocks");
        }
    }
}