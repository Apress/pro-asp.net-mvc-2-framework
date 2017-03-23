<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<PersonEditorExample.Models.Person>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Index</title>
</head>
<body>
    <div>
        <h2>Edit this person</h2>
        <% using(Html.BeginForm()) { %>
            <fieldset>
                <legend>Person</legend>
                <div class="field">
                    <label>Name:</label>
                    <%: Html.EditorFor(x => x.FirstName) %>
                    <%: Html.EditorFor(x => x.LastName) %>
                </div>
                <div class="field">
                    <label>Born:</label>
                    <%: Html.EditorFor(x => x.BirthDate) %>
                </div>
                <div align="center"><%: Html.EditorFor(x => x.IsApproved) %>May log in</div>
                <fieldset>
                    <legend>Home address</legend>
                    <div class="addressEditor">
                    <%: Html.EditorFor(x => x.HomeAddress) %>
                    </div>
                </fieldset>
            </fieldset>
            <p><input type="submit" value="Save" /></p>
        <% } %>
    </div>
</body>
</html>