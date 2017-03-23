<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<PersonEditorExample.Models.Person>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Display</title>
    <link rel="Stylesheet" href="../../Content/styles.css" />
</head>
<body>
    <div>
        <h2>Examine this person</h2>
        <%: Html.DisplayForModel() %>
    </div>
</body>
</html>
