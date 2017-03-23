<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SportsStore.Domain.Entities.Cart>" %>

<% if (Model.Lines.Count > 0) { %>
    <div id="cart">
        <span class="caption">
            <b>Your cart:</b>
            <%= Html.Encode(Model.Lines.Sum(x => x.Quantity)) %> item(s),
            <%= Html.Encode(Model.ComputeTotalValue().ToString("c")) %>
            </span>
        <%= Html.ActionLink("Check out", "Index", "Cart", new { returnUrl = Request.Url.PathAndQuery }, null) %>
    </div>
<% } %>