<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<AlternativeViewEngines.Models.Mountain>>" %>
<%@ Import Namespace="MvcContrib.ViewEngines" %>
<h2><%= ViewData["message"] %></h2>

<p>Here's some data</p>

<table width="50%" border="1">
  <thead>
    <tr>
      <th>Name</th>
      <th>Height (m)</th>
      <th>Date discovered</th>
    </tr>
  </thead>
  <% foreach (var mountain in Model) { %>
    <tr>
        <td><%= mountain.Name %></td>
        <td><%= mountain.Height %></td>
        <td><%= mountain.DateDiscovered.ToShortDateString() %></td>
    </tr>    
  <% } %>
</table>

<form action="<%= Url.Action("SubmitEmail") %>" method="post">
  E-mail:  <%= Html.TextBox("email") %>
  <input type="submit" value="Subscribe" />
</form>
