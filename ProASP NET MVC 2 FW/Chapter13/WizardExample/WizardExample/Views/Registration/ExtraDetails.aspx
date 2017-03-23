<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<WizardExample.Models.RegistrationData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ExtraDetails
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Registration: Extra details</h2>
Just a bit more info please.

<% using(Html.BeginForm()) { %>
    <%: Html.Serialize("regData", Model) %>
    <%: Html.ValidationSummary() %>
    <p>Age: <%: Html.EditorFor(x => x.Age) %></p>
    <p>Hobbies: <%: Html.TextAreaFor(x => x.Hobbies) %></p>
    <p>
        <input type="submit" name="backButton" value="< Back" />
        <input type="submit" name="nextButton" value="Next >" />
    </p>        
<% } %>


</asp:Content>
