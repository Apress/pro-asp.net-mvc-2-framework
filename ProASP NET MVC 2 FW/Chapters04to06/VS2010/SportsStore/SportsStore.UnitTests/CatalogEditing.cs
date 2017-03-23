using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;

namespace SportsStore.UnitTests
{
    [TestFixture]
    public class CatalogEditing
    {
        [Test]
        public void Can_View_List_Of_All_Products()
        {
            // Arrange: Repository that can return any IQueryable<Product>
            var testProducts = new[] { new Product { ProductID = 123 }, new Product { ProductID = 456 } };
            var mockRepository = UnitTestHelpers.MockProductsRepository(testProducts);

            // Act
            var result = new AdminController(mockRepository).Index();

            // Assert
            result.ShouldBeDefaultView();
            CollectionAssert.AreEquivalent(testProducts, (IEnumerable)result.ViewData.Model);
        }

        [Test]
        public void Can_Access_Product_Editing_Screen()
        {
            // Arrange: Repository that can return any IQueryable<Product>
            var testProducts = new[] { new Product { ProductID = 12 }, new Product { ProductID = 17 } };
            var mockRepository = UnitTestHelpers.MockProductsRepository(testProducts);

            // Act
            var result = new AdminController(mockRepository).Edit(17);

            // Assert
            result.ShouldBeDefaultView();
            ((Product)result.ViewData.Model).ProductID.ShouldEqual(17);
        }

        [Test]
        public void Can_Save_Edited_Product()
        {
            // Arrange: Given a repository containing a product
            var mockRepository = new Mock<IProductsRepository>();
            var product = new Product { ProductID = 123 };
            mockRepository.Setup(x => x.Products).Returns(new[] { product }.AsQueryable());

            // Act: When a user tries to save valid data using this product's ID
            var controller = new AdminController(mockRepository.Object)
                .WithIncomingValues(new FormCollection {
                    { "Name", "SomeName" }, { "Description", "SomeDescription" },
                    { "Price", "1" }, { "Category", "SomeCategory" }
                });
            var result = controller.Edit(123, null);

            // Assert: Then the product is saved and the user is suitably redirected
            mockRepository.Verify(x => x.SaveProduct(product));
            result.ShouldBeRedirectionTo(new { action = "Index" });
        }

        [Test]
        public void Cannot_Save_Invalid_Product()
        {
            // Arrange: Given a repository and a product
            var mockRepository = new Mock<IProductsRepository>();
            var product = new Product { ProductID = 123 };
            mockRepository.Setup(x => x.Products).Returns(new[] { product }.AsQueryable());
            mockRepository.Setup(x => x.SaveProduct(It.IsAny<Product>())).Callback(Assert.Fail); // To ensure it isn't called

            // Act: When a user tries to save an invalid product
            var controller = new AdminController(mockRepository.Object).WithIncomingValues(new FormCollection());
            var result = controller.Edit(123, null);

            // Assert: Then they are kept on the editor screen
            result.ShouldBeDefaultView();
        }

        [Test]
        public void Can_Create_New_Product()
        {
            var result = new AdminController(null).Create();
            result.ShouldBeView("Edit");
        }

        [Test]
        public void Can_Delete_Product()
        {
            // Arrange: Given a repository containing some product...
            var mockRepository = new Mock<IProductsRepository>();
            var product = new Product { ProductID = 24, Name = "P24" };
            mockRepository.Setup(x => x.Products).Returns(new[] { product }.AsQueryable());

            // Act: ... when the user tries to delete that product
            var controller = new AdminController(mockRepository.Object);
            var result = controller.Delete(24);

            // Assert: ... then it's deleted, and the user sees a confirmation
            mockRepository.Verify(x => x.DeleteProduct(product));
            result.ShouldBeRedirectionTo(new { action = "Index" });
            controller.TempData["message"].ShouldEqual("P24 was deleted");
        }
    }
}