using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests
{
    [TestFixture]
    public class NavigationByCategory
    {
        [Test]
        public void NavMenu_Includes_Alphabetical_List_Of_Distinct_Categories()
        {
            // Arrange: Given 4 products in 3 categories in nonalphabetized order
            var mockProductsRepository = UnitTestHelpers.MockProductsRepository(
                new Product { Category = "Vegetable", Name = "ProductA" },
                new Product { Category = "Animal", Name = "ProductB" },
                new Product { Category = "Vegetable", Name = "ProductC" },
                new Product { Category = "Mineral", Name = "ProductD" }
            );

            // Act: ... when we render the navigation menu
            var result = new NavController(mockProductsRepository).Menu(null);
            
            // Assert: ... then the links to categories ...
            var categoryLinks = ((IEnumerable<NavLink>)result.ViewData.Model)
                                .Where(x => x.RouteValues["category"] != null);

            // ... are distinct categories in alphabetical order
            CollectionAssert.AreEqual(
                new[] { "Animal", "Mineral", "Vegetable" },          // Expected
                categoryLinks.Select(x => x.RouteValues["category"]) // Actual
            );

            // ... and contain enough information to link to that category
            foreach (var link in categoryLinks) {
                link.RouteValues["controller"].ShouldEqual("Products");
                link.RouteValues["action"].ShouldEqual("List");
                link.RouteValues["page"].ShouldEqual(1);
                link.Text.ShouldEqual(link.RouteValues["category"]);
            }
        }

        [Test]
        public void NavMenu_Shows_Home_Link_At_Top()
        {
            // Arrange: Given any repository
            var mockProductsRepository = UnitTestHelpers.MockProductsRepository();

            // Act: ... when we render the navigation menu
            var result = new NavController(mockProductsRepository).Menu(null);
            
            // Assert: ... then the top link is to Home
            var topLink = ((IEnumerable<NavLink>)result.ViewData.Model).First();
            topLink.RouteValues["controller"].ShouldEqual("Products");
            topLink.RouteValues["action"].ShouldEqual("List");
            topLink.RouteValues["page"].ShouldEqual(1);
            topLink.RouteValues["category"].ShouldEqual(null);
            topLink.Text.ShouldEqual("Home");
        }

        [Test]
        public void NavMenu_Highlights_Current_Category()
        {
            // Arrange: Given two categories...
            var mockProductsRepository = UnitTestHelpers.MockProductsRepository(
            new Product { Category = "A", Name = "ProductA" },
            new Product { Category = "B", Name = "ProductB" }
            );
            
            // Act: ... when we render the navigation menu
            var result = new NavController(mockProductsRepository).Menu("B");

            // Assert: ... then only the current category is highlighted
            var highlightedLinks = ((IEnumerable<NavLink>)result.ViewData.Model)
            .Where(x => x.IsSelected).ToList();
            highlightedLinks.Count.ShouldEqual(1);
            highlightedLinks[0].Text.ShouldEqual("B");
        }
    }
}