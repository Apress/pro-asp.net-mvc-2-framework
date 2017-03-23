<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="HtmlFilteringDemo"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
    <head runat="server">
        <title>Index</title>
    </head>
    <body>
        Here's some filtered HTML:
          <%: HtmlFilter.Filter("<b>Hello</b> <u><i>world</i></u><script>alert('X');</script>",
                    new string[] { "b", "i", "div", "span" }) /* Only allow these tags */ %>
    </body>
</html>
