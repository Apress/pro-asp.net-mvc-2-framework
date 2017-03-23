<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<WizardExample.Models.RegistrationData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	BasicDetails
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Registration: Basic details</h2>
Please enter your details

<% using(Html.BeginForm()) { %>
    <%: Html.Serialize("regData", Model) %>
    <%: Html.ValidationSummary() %>
    <p>Name: <%: Html.EditorFor(x => x.Name)  %></p>
    <p>E-mail: <%: Html.EditorFor(x => x.Email) %></p>
    <p><input type="submit" name="nextButton" value="Next >" /></p>
<% } %>


</asp:Content>
