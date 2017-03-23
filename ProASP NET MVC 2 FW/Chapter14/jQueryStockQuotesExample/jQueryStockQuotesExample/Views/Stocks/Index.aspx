<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Regular JSON version -->
    <h2>Stocks</h2>
    <% using(Html.BeginForm("GetQuote", "Stocks")) { %>
        Symbol:
        <%: Html.TextBox("symbol") %>
        <input type="submit" />
    <% } %>
    <hr />

    <!-- JSONP version -->
    <h2>Stocks - JSONP version</h2>
    <form action="/Stocks/GetQuoteJsonP">
        Symbol:
        <%: Html.TextBox("symbol") %>    
        <input type="submit" />
    </form>
    <hr />

    <!-- XML version -->
    <h2>Stocks - XML version</h2>
    <form action="/Stocks/GetQuoteXml">
        Symbol:
        <%: Html.TextBox("symbol") %>    
        <input type="submit" />
    </form>
    <hr />

    <table>
        <tr><td>Opening price:</td><td><div id="openingPrice" /></td></tr>
        <tr><td>Closing price:</td><td><div id="closingPrice" /></td></tr>
        <tr><td>Rating:</td><td><div id="stockRating" /></td></tr>
    </table>
    <p><i>This page generated at <%: DateTime.Now.ToLongTimeString() %></i></p>

    <script type="text/javascript">
        // Regular JSON version
        $("form[action$='GetQuote']").submit(function () {
            $.ajax({
                url: $(this).attr("action"),
                type: "post",
                data: $(this).serialize(),
                success: function (stockData) {
                    $("#openingPrice").html(stockData.OpeningPrice).hide().fadeIn();
                    $("#closingPrice").html(stockData.ClosingPrice).hide().fadeIn();
                    $("#stockRating").html(stockData.Rating).hide().fadeIn();
                }
            });
            return false;
        });

        // JSONP version
        $("form[action$='GetQuoteJsonP']").submit(function () {
            $.ajax({
                url: $(this).attr("action"),
                data: $(this).serialize(),
                dataType: "jsonp",
                success: function (stockData) {
                    $("#openingPrice").html(stockData.OpeningPrice);
                    $("#closingPrice").html(stockData.ClosingPrice);
                    $("#stockRating").html(stockData.Rating);
                }
            });
            return false;
        });

        // XML version
        $("form[action$='GetQuoteXml']").submit(function () {
            $.ajax({
                url: $(this).attr("action"),
                data: $(this).serialize(),
                dataType: "xml", // Instruction to parse response as XMLDocument
                success: function (resultXml) {
                    // Extract data from XMLDocument using jQuery selectors
                    var opening = $("OpeningPrice", resultXml).text();
                    var closing = $("ClosingPrice", resultXml).text();
                    var rating = $("Rating", resultXml).text();
                    // Use that data to update DOM
                    $("#openingPrice").html(opening);
                    $("#closingPrice").html(closing);
                    $("#stockRating").html(rating);
                }
            });
            return false;
        });

    </script>
</asp:Content>
