using System;
using System.Linq;
using BookingsExample.Domain.Models;

namespace BookingsExample.Domain.Services
{
    public class AppointmentService
    {
        public static void CreateAppointment(Appointment appt)
        {
            EnsureValidForCreation(appt);
            // To do: Now save the appointment to a database or wherever
        }

        private static void EnsureValidForCreation(Appointment appt)
        {
            var errors = new RulesException<Appointment>();

            if (string.IsNullOrEmpty(appt.ClientName))
                errors.ErrorFor(x => x.ClientName, "Please specify a name");
            
            if (appt.AppointmentDate < DateTime.Now.Date)
                errors.ErrorFor(x => x.AppointmentDate, "Can't book in the past");
            else if ((appt.AppointmentDate - DateTime.Now.Date).TotalDays > 7)
                errors.ErrorFor(x => x.AppointmentDate, "Can't book more than a week in advance");
            
            if (appt.ClientName == "Steve" && appt.AppointmentDate.DayOfWeek == DayOfWeek.Saturday)
                errors.ErrorForModel("Steve can't book on weekends");

            if (errors.Errors.Any())
                throw errors;
        }
    }
}