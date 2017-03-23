using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using GridExample.Models;

namespace GridExample.Controllers
{
    public class SortableMountainsController : Controller
    {
        public ViewResult Index()
        {
            // Display them in a random order
            var rng = new Random();
            return View(SampleData.SevenSummits.OrderBy(x => rng.Next(int.MaxValue)));
        }

        [HttpPost]
        public string Index(string chosenOrder)
        {
            string correctOrder = SampleData.SevenSummits
                .OrderByDescending(x => x.HeightInMeters)
                .Aggregate("", (str, mountain) => str + mountain.Name + "|");
            return correctOrder == chosenOrder ? "You're right!" : "Sorry, you're wrong.";         
        }
    }
}