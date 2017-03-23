using System;
using System.Web.Mvc;
using BookingsExample.Domain;
using BookingsExample.Domain.Models;
using BookingsExample.Domain.Services;

namespace BookingsExample.Controllers
{
    public class BookingController : Controller
    {
        public ViewResult MakeBooking()
        {
            var initialState = new Appointment {
                AppointmentDate = DateTime.Now.Date
            };

            return View(initialState);
        }

        [HttpPost]
        public ActionResult MakeBooking(Appointment appt, bool acceptsTerms)
        {
            if (!acceptsTerms)
                ModelState.AddModelError("acceptsTerms", "You must accept the terms");
            
            try {
                if (ModelState.IsValid) // Not worth trying if we already know it's bad
                    AppointmentService.CreateAppointment(appt);
            }
            catch (RulesException ex) {
                ex.CopyTo(ModelState); // To be implemented in a moment
            }

            if (ModelState.IsValid)
                return View("Completed", appt);
            else
                return View(); // Re-renders the same view so the user can fix the errors
        }
    }
}