using System;
using System.Web.Mvc;
using PersonEditorExample.Models;

namespace PersonEditorExample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Display", new { personId = 683 });
        }

        public ActionResult Display(int personId)
        {
            return View(GetPerson(personId));
        }


        public ActionResult Edit(int personId)
        {
            return View(GetPerson(personId));
        }

        private static Person GetPerson(int personId)
        {
            // Could load from a database or whatever
            return new Person {
                PersonId = personId,
                FirstName = "Blaise",
                LastName = "Pascal",
                BirthDate = new DateTime(1623, 6, 19),
                IsApproved = true,
                HomeAddress = new Address {
                    Line1 = "15 Rue Pensee",
                    Line2 = "Clermont-Ferrand",
                    City = "Paris",
                    PostalCode = "27574",
                    Country = "France"
                }
            };
        }
    }
}
