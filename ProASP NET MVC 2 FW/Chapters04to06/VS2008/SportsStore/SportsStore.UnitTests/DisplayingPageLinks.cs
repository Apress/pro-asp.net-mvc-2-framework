using System;
using System.Web.Mvc;
using NUnit.Framework;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests
{
    using SportsStore.WebUI.HtmlHelpers; // The extension will go in this namespace

    [TestFixture]
    public class DisplayingPageLinks
    {
        [Test]
        public void Can_Generate_Links_To_Other_Pages()
        {
            // Arrange: We're going to extend the HtmlHelper class.
            // It doesn't matter if the variable we use is null.
            HtmlHelper html = null;
            // Arrange: The helper should take a PagingInfo instance (that's
            // a class we haven't yet defined) and a lambda to specify the URLs
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };
            Func<int, string> pageUrl = i => "Page" + i;
            // Act
            MvcHtmlString result = html.PageLinks(pagingInfo, pageUrl);
            // Assert: Here's how it should format the links
            result.ToString().ShouldEqual(@"<a href=""Page1"">1</a>
<a class=""selected"" href=""Page2"">2</a>
<a href=""Page3"">3</a>
");
        }
        [Test]
        public void Product_Lists_Include_Correct_Page_Numbers()
        {
            // Arrange: If there are five products in the repository...
            var mockRepository = UnitTestHelpers.MockProductsRepository(
            new Product { Name = "P1" }, new Product { Name = "P2" },
            new Product { Name = "P3" }, new Product { Name = "P4" },
            new Product { Name = "P5" }
            );
            var controller = new ProductsController(mockRepository) { PageSize = 3 };
            
            // Act: ... then when the user asks for the second page (PageSize=3)...
            var result = controller.List(null, 2);

            // Assert: ... they'll see page links matching the following
            var viewModel = (ProductsListViewModel)result.ViewData.Model;
            PagingInfo pagingInfo = viewModel.PagingInfo;
            pagingInfo.CurrentPage.ShouldEqual(2);
            pagingInfo.ItemsPerPage.ShouldEqual(3);
            pagingInfo.TotalItems.ShouldEqual(5);
            pagingInfo.TotalPages.ShouldEqual(2);
        }
    }
}