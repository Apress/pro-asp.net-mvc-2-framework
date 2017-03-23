using System;
using System.ComponentModel.DataAnnotations;

namespace BookingsExample.Domain.Models
{
    public class Appointment
    {
        [Required(ErrorMessage = "Please enter your name")] [StringLength(50)]
        public string ClientName { get; set; }

        [DataType(DataType.Date)] [Required(ErrorMessage = "Please choose a date")]
        public DateTime AppointmentDate { get; set; }
    }
}