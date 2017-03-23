<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<GridExample.Models.MountainInfo>>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#summits tr:even").addClass("alternate");

            $("#summits form[action$='/DeleteItem']").live("submit", function () {
                var itemText = $("input[name='item']", this).val();
                return confirm("Are you sure you want to delete '" + itemText + "'?");
            });

            $("<label><input id='heights' type='checkbox'/>Show heights</label>")
                .insertBefore("#summits")
                .children("input").click(function () {
                    $("#summits td:nth-child(2)").toggle(this.checked);
                });
            $("#summits td:nth-child(2)").hide();

            $("#summits a").live("click", function () {
                $.get($(this).attr("href"), function (response) {
                    $("#summits").replaceWith($("#summits", response));

                    // Reapply zebra striping
                    $("#summits tr:even").addClass("alternate");

                    // Respect the (un)checked state of the "show heights" check box
                    $("#summits td:nth-child(2)").toggle($("#heights")[0].checked);
                });
                return false;
            });

        });
    </script>

    <h2>The Seven Summits</h2>
    <div id="summits">
        <table>
            <thead><tr>
                <td>Item</td> <td>Height (m)</td> <td>Actions</td>
            </tr></thead>
            <% foreach(var mountain in Model) { %>
                <tr>
                    <td><%: mountain.Name %></td>
                    <td><%: mountain.HeightInMeters %></td>
                    <td>
                        <% using(Html.BeginForm("DeleteItem", "Home")) { %>
                            <%: Html.Hidden("item", mountain.Name) %>
                            <input type="submit" value="Delete" />
                        <% } %>
                    </td>
                </tr>
            <% } %>
        </table>
    Page:
    <%: Html.PageLinks((PagingInfo)ViewData["pagingInfo"], 
                       i => Url.Action("Summits", new { page = i })) %>
</div>
<p><i>This page generated at <%: DateTime.Now.ToLongTimeString() %></i></p>

</asp:Content>
