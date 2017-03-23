using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace PartyInvites.Models
{
    public class GuestResponse
    {
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter your email address")]
        [RegularExpression(".+\\@.+\\..+",
        ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your phone number")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please specify whether you'll attend")]
        public bool? WillAttend { get; set; }

        public void Submit()
        {
            // If you want the application to actually send an email, uncomment the following line, and ensure
            // the <smtp> configuration node in Web.config refers to a valid SMTP host or pickup directory
            //new SmtpClient().Send(BuildMailMessage());
        }

        private MailMessage BuildMailMessage()
        {
            var message = new StringBuilder();
            message.AppendFormat("Date: {0:yyyy-MM-dd hh:mm}\n", DateTime.Now);
            message.AppendFormat("RSVP from: {0}\n", Name);
            message.AppendFormat("Email: {0}\n", Email);
            message.AppendFormat("Phone: {0}\n", Phone);
            message.AppendFormat("Can come: {0}\n", WillAttend.Value ? "Yes" : "No");
            return new MailMessage(
            "rsvps@example.com", // From
            "party-organizer@example.com", // To
            Name + (WillAttend.Value ? " will attend" : " won't attend"), // Subject
            message.ToString() // Body
            );
        }
    }
}