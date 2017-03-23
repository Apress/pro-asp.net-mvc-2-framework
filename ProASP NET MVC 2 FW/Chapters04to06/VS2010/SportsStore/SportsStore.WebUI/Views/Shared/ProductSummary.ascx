<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SportsStore.Domain.Entities.Product>" %>

<div class="item">
    <% if(Model.ImageData != null) { %>
        <div style="float:left; margin-right:20px">
            <img src="<%: Url.Action("GetImage", "Products", new { Model.ProductID }) %>" />
        </div>
    <% } %>

    <h3><%: Model.Name %></h3>
    <%: Model.Description %>

    <% using(Html.BeginForm("AddToCart", "Cart")) { %>
        <%: Html.HiddenFor(x => x.ProductID) %>
        <%: Html.Hidden("returnUrl", Request.Url.PathAndQuery) %>
        <input type="submit" value="+ Add to cart" />
    <% } %>

    <h4><%: Model.Price.ToString("c") %></h4>
</div>