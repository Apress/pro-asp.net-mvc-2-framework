<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="ViewPage<SportsStore.WebUI.Models.ProductsListViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SportsStore : <%: Model.CurrentCategory ?? "All Products" %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% foreach(var product in Model.Products) { %>
        <% Html.RenderPartial("ProductSummary", product); %>
    <% } %>

    <div class="pager">
        <%: Html.PageLinks(Model.PagingInfo,
            x => Url.Action("List", new {page=x, category=Model.CurrentCategory})) %>
    </div>
</asp:Content>
