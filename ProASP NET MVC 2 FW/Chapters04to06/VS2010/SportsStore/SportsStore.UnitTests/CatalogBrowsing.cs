using System.Collections.Generic;
using NUnit.Framework;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests
{
    [TestFixture]
    public class CatalogBrowsing
    {
        [Test]
        public void Can_View_A_Single_Page_Of_Products()
        {
            // Arrange: If there are 5 products in the repository...
            IProductsRepository repository = UnitTestHelpers.MockProductsRepository(
            new Product { Name = "P1" }, new Product { Name = "P2" },
            new Product { Name = "P3" }, new Product { Name = "P4" },
            new Product { Name = "P5" }
            );
            var controller = new ProductsController(repository);
            controller.PageSize = 3; 
            
            // Act: ... then when the user asks for the second page (PageSize=3)...
            var result = controller.List(null, 2);

            // Assert: ... they'll just see the last two products.
            var viewModel = (ProductsListViewModel)result.ViewData.Model;
            var displayedProducts = viewModel.Products;
            displayedProducts.Count.ShouldEqual(2);
            displayedProducts[0].Name.ShouldEqual("P4");
            displayedProducts[1].Name.ShouldEqual("P5");
        }

        [Test]
        public void Can_View_Products_From_All_Categories()
        {
            // Arrange: If two products are in two different categories...
            IProductsRepository repository = UnitTestHelpers.MockProductsRepository(
            new Product { Name = "Artemis", Category = "Greek" },
            new Product { Name = "Neptune", Category = "Roman" }
            );
            var controller = new ProductsController(repository);

            // Act: ... then when we ask for the "All Products" category
            var result = controller.List(null, 1);

            // Arrange: ... we get both products
            var viewModel = (ProductsListViewModel)result.ViewData.Model;
            viewModel.Products.Count.ShouldEqual(2);
            viewModel.Products[0].Name.ShouldEqual("Artemis");
            viewModel.Products[1].Name.ShouldEqual("Neptune");
        }

        [Test]
        public void Can_View_Products_From_A_Single_Category()
        {
            // Arrange: If two products are in two different categories...
            IProductsRepository repository = UnitTestHelpers.MockProductsRepository(
                new Product { Name = "Artemis", Category = "Greek" },
                new Product { Name = "Neptune", Category = "Roman" }
            );
            var controller = new ProductsController(repository);

            // Act: ... then when we ask for one specific category
            var result = controller.List("Roman", 1);
            
            // Arrange: ... we get only the product from that category
            var viewModel = (ProductsListViewModel) result.ViewData.Model;
            viewModel.Products.Count.ShouldEqual(1);
            viewModel.Products[0].Name.ShouldEqual("Neptune");
            viewModel.CurrentCategory.ShouldEqual("Roman");
        }
    }
}