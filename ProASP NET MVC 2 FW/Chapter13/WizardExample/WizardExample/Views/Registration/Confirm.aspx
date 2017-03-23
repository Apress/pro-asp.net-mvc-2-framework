<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<WizardExample.Models.RegistrationData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Confirm
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Confirm</h2>
Please confirm that your details are correct.
<% using(Html.BeginForm()) { %>
    <%: Html.Serialize("regData", Model) %>
    <div>Name: <b><%: Model.Name %></b></div>
    <div>E-mail: <b><%: Model.Email %></b></div>
    <div>Age: <b><%: Model.Age %></b></div>
    <div>Hobbies: <b><%: Model.Hobbies %></b></div>

    <p>
        <input type="submit" name="backButton" value="< Back" />
        <input type="submit" name="nextButton" value="Next >" />
    </p> 
<% } %>


</asp:Content>
