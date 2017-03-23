<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="CaptchaExample.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Registration</h2>
<% using(Html.BeginForm("SubmitRegistration", "Registration")) { %>
    Please register. It's worth it.
    <i>To do: Ask for account details (name, address, pet's name, Gmail password, etc.)</i>
    
    <h3>Verification</h3>
    <p>Please enter the letters displayed below.</p>
    <%= Html.Captcha("myCaptcha") %>
    <div>Verification letters: <%= Html.TextBox("attempt") %></div>
    <%= Html.ValidationMessage("attempt") %>
    
    <p><input type="submit" value="Submit registration" /></p>
<% } %>


</asp:Content>
