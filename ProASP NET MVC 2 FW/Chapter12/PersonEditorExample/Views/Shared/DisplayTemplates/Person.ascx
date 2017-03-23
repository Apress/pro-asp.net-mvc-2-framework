<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PersonEditorExample.Models.Person>" %>

<div class="person">
    <img class="person-icon" src="/content/person-icon.png" />
    <%: Html.ActionLink(Model.FirstName + " " + Model.LastName, "Edit", new { Model.PersonId }) %>
    (born <%: Model.BirthDate.ToString("MMMM dd, yyyy") %>)

    <div class="address">
        <%: Html.DisplayFor(x => x.HomeAddress, "AddressSingleLine") %>
    </div>
</div>