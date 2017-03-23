<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<BookingsExample.Domain.Models.Appointment>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>MakeBooking</title>
    <link rel="Stylesheet" href="../../Content/Site.css" />
    <script src="../../Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="../../Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
</head>
<body>
    <div>
        <h1>Book an appointment</h1>

        <% Html.EnableClientValidation(); %>
        <% using(Html.BeginForm()) { %>
            <%: Html.ValidationSummary(true) %>
            <p>
                Your name: <%: Html.EditorFor(x => x.ClientName) %>
                <%: Html.ValidationMessageFor(x => x.ClientName) %>
            </p>
            <p>
                Appointment date:
                <%:Html.EditorFor(x => x.AppointmentDate)%>
                <%: Html.ValidationMessageFor(x => x.AppointmentDate)%>
            </p>
            <p>
                <%: Html.CheckBox("acceptsTerms") %>
                <label for="acceptsTerms">I accept the Terms of Booking</label>
                <%: Html.ValidationMessage("acceptsTerms") %>
            </p>

            <input type="submit" value="Place booking" />
        <% } %>
    </div>
</body>
</html>
