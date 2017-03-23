using System.Net.Mail;
using System.Text;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Services
{
    public class EmailOrderSubmitter : IOrderSubmitter
    {
        private string mailTo;
        public EmailOrderSubmitter(string mailTo)
        {
            this.mailTo = mailTo;
        }
        
        public void SubmitOrder(Cart cart, ShippingDetails shippingDetails)
        {
            // If you want the application to actually send email, uncomment the following lines and ensure the <smtp> node
            // in Web.config refers to a valid SMTP host or pickup directory
            /*
            using (var smtpClient = new SmtpClient())
            using (var mailMessage = BuildMailMessage(cart, shippingDetails))
            {
                smtpClient.Send(mailMessage);
            }
            */
        }

        private MailMessage BuildMailMessage(Cart cart, ShippingDetails shippingDetails)
        {
            StringBuilder body = new StringBuilder();
            body.AppendLine("A new order has been submitted");
            body.AppendLine("---");
            body.AppendLine("Items:");
            foreach (var line in cart.Lines)
            {
                var subtotal = line.Product.Price * line.Quantity;
                body.AppendFormat("{0} x {1} (subtotal: {2:c}", line.Quantity,
                                  line.Product.Name,
                                  subtotal);
            }
            body.AppendFormat("Total order value: {0:c}", cart.ComputeTotalValue());
            body.AppendLine("---");
            body.AppendLine("Ship to:");
            body.AppendLine(shippingDetails.Name);
            body.AppendLine(shippingDetails.Line1);
            body.AppendLine(shippingDetails.Line2 ?? "");
            body.AppendLine(shippingDetails.Line3 ?? "");
            body.AppendLine(shippingDetails.City);
            body.AppendLine(shippingDetails.State ?? "");
            body.AppendLine(shippingDetails.Country);
            body.AppendLine(shippingDetails.Zip);
            body.AppendLine("---");
            body.AppendFormat("Gift wrap: {0}", shippingDetails.GiftWrap ? "Yes" : "No");
            return new MailMessage("sportsstore@example.com",   // From
                                   mailTo,                      // To
                                   "New order submitted!",      // Subject
                                   body.ToString());            // Body
        }
    }
}