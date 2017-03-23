<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="System.Threading" %>

<h1><%: Thread.CurrentThread.CurrentUICulture.TextInfo.ToTitleCase(Resources.Greeting) %>!</h1>

<p> Latest news: </p>

<ul>
    <li>Government plans to widen <%: Resources.Sidewalk %></li>
    <li>Man's <%: Resources.Pants %> caught in <%: Resources.Elevator %> door</li>
    <li>Christmas is <%: new DateTime(2010, 12, 25).ToShortDateString() %></li>
    <li>One unit of currency is <%: string.Format("{0:c}", 1)%></li>
</ul>

<p>With respect to <%: Resources.TheRuler %>.</p>