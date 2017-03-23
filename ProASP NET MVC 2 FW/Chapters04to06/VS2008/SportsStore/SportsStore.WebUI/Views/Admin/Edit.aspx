<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<SportsStore.Domain.Entities.Product>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Admin : Edit <%= Html.Encode(Model.Name) %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Edit <%= Html.Encode(Model.Name) %></h1>

    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm("Edit", "Admin", FormMethod.Post,
                             new { enctype = "multipart/form-data" })) { %>
        <%= Html.EditorForModel() %>
        <div class="editor-label">Image</div>
        <div class="editor-field">
            <% if (Model.ImageData == null) { %>
                None
            <% } else { %>
                <img src="<%= Html.Encode(Url.Action("GetImage", "Products", new { Model.ProductID })) %>" />
            <% } %>
            <div>Upload new image: <input type="file" name="Image" /></div>
        </div>

        <input type="submit" value="Save" />
        <%= Html.ActionLink("Cancel and return to List", "Index") %>
    <% } %>

</asp:Content>
