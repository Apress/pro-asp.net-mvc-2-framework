using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GridExample.HtmlHelpers;
using GridExample.Models;

namespace GridExample.Controllers
{
    public class HomeController : Controller
    {
        private const int PageSize = 3;

        public ActionResult Index()
        {
            return RedirectToAction("Summits");
        }

        public ActionResult Summits([DefaultValue(1)] int page)
        {
            var allItems = SampleData.SevenSummits;

            ViewData["pagingInfo"] = new PagingInfo {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = allItems.Count
            };

            return View(allItems.Skip((page - 1)*PageSize).Take(PageSize));
        }

        public string DeleteItem(string item)
        {
            return "OK, I'm deleting " + HttpUtility.HtmlEncode(item);
        }
    }
}
