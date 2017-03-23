<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SportsStore.WebUI.Models.CartIndexViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SportsStore : Your Cart
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Your cart</h2>

    <table width="90%" align="center">
        <thead>
            <tr>
                <th align="center">Quantity</th>
                <th align="left">Item</th>
                <th align="right">Price</th>
                <th align="right">Subtotal</th>
            </tr>
        </thead>
        <tbody>
            <% foreach(var line in Model.Cart.Lines) { %>
                <tr>
                    <td align="center"><%= Html.Encode(line.Quantity) %></td>
                    <td align="left"><%= Html.Encode(line.Product.Name) %></td>
                    <td align="right"><%= Html.Encode(line.Product.Price.ToString("c")) %></td>
                    <td align="right">
                        <%= Html.Encode((line.Quantity*line.Product.Price).ToString("c")) %>
                    </td>
                    <td>
                        <% using(Html.BeginForm("RemoveFromCart", "Cart")) { %>
                            <%= Html.Hidden("ProductId", line.Product.ProductID) %>
                            <%= Html.HiddenFor(x => x.ReturnUrl) %>
                            <input type="submit" value="Remove" />
                        <% } %>
                    </td>
                </tr>
            <% } %>
        </tbody>
        <tfoot>
            <tr>
                <td colspan="3" align="right">Total:</td>
                <td align="right">
                    <%= Html.Encode(Model.Cart.ComputeTotalValue().ToString("c")) %>
                </td>
            </tr>
        </tfoot>
    </table>

    <p align="center" class="actionButtons">
        <a href="<%= Html.Encode(Model.ReturnUrl) %>">Continue shopping</a>
        <%= Html.ActionLink("Check out now", "CheckOut") %>
    </p>
</asp:Content>