using System.Linq;
using Moq;
using NUnit.Framework;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Services;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests
{
    [TestFixture]
    public class ShoppingCart
    {
        [Test]
        public void Cart_Starts_Empty()
        {
            new Cart().Lines.Count.ShouldEqual(0);
        }

        [Test]
        public void Cart_Combines_Lines_With_Same_Product()
        {
            // Arrange: Given we have two products
            Product p1 = new Product { ProductID = 1 };
            Product p2 = new Product { ProductID = 2 };
            // Act: ... when we add them to a cart multiple times
            var cart = new Cart();
            cart.AddItem(p1, 1);
            cart.AddItem(p1, 2);
            cart.AddItem(p2, 10);
            // Assert: ... then lines combine quantities for distinct products
            cart.Lines.Count.ShouldEqual(2);
            cart.Lines.First(x => x.Product.ProductID == 1).Quantity.ShouldEqual(3);
            cart.Lines.First(x => x.Product.ProductID == 2).Quantity.ShouldEqual(10);
        }

        [Test]
        public void Cart_Can_Be_Cleared()
        {
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            cart.Clear();
            cart.Lines.Count.ShouldEqual(0);
        }

        [Test]
        public void Cart_TotalValue_Is_Sum_Of_Price_Times_Quantity()
        {
            Cart cart = new Cart();
            cart.AddItem(new Product { ProductID = 1, Price = 5 }, 10);
            cart.AddItem(new Product { ProductID = 2, Price = 2.1M }, 3);
            cart.AddItem(new Product { ProductID = 3, Price = 1000 }, 1);
            cart.ComputeTotalValue().ShouldEqual(1056.3M);
        }

        [Test]
        public void Can_Add_Product_To_Cart()
        {
            // Arrange: Given a repository with some products...
            var mockProductsRepository = UnitTestHelpers.MockProductsRepository(
                new Product { ProductID = 123 },
                new Product { ProductID = 456 }
            );
            var cartController = new CartController(mockProductsRepository, null);
            var cart = new Cart();

            // Act: When a user adds a product to their cart...
            cartController.AddToCart(cart, 456, null);

            // Assert: Then the product is in their cart
            cart.Lines.Count.ShouldEqual(1);
            cart.Lines[0].Product.ProductID.ShouldEqual(456);
        }

        [Test]
        public void After_Adding_Product_To_Cart_User_Goes_To_Your_Cart_Screen()
        {
            // Arrange: Given a repository with some products...
            var mockProductsRepository = UnitTestHelpers.MockProductsRepository(
                new Product { ProductID = 1 }
            );
            var cartController = new CartController(mockProductsRepository, null);

            // Act: When a user adds a product to their cart...
            var result = cartController.AddToCart(new Cart(), 1, "someReturnUrl");

            // Assert: Then the user is redirected to the Cart Index screen
            result.ShouldBeRedirectionTo(new
            {
                action = "Index",
                returnUrl = "someReturnUrl"
            });
        }

        [Test]
        public void Can_View_Cart_Contents()
        {
            // Arrange/act: Given the user vists CartController's Index action...
            var cart = new Cart();
            var result = new CartController(null, null).Index(cart, "someReturnUrl");

            // Assert: Then the view has their cart and the correct return URL
            var viewModel = (CartIndexViewModel)result.ViewData.Model;
            viewModel.Cart.ShouldEqual(cart);
            viewModel.ReturnUrl.ShouldEqual("someReturnUrl");
        }

        [Test]
        public void Cannot_Check_Out_If_Cart_Is_Empty()
        {
            // Arrange/act: When a user tries to check out with an empty cart
            var emptyCart = new Cart();
            var shippingDetails = new ShippingDetails();
            var result = new CartController(null, null) .CheckOut(emptyCart, shippingDetails);

            // Assert
            result.ShouldBeDefaultView();
        }

        [Test]
        public void Cannot_Check_Out_If_Shipping_Details_Are_Invalid()
        {
            // Arrange: Given a user has a non-empty cart
            var cart = new Cart();
            cart.AddItem(new Product(), 1);

            // Arrange: ... but the shipping details are invalid
            var cartController = new CartController(null, null);
            cartController.ModelState.AddModelError("any key", "any error");
            
            // Act: When the user tries to check out
            var result = cartController.CheckOut(cart, new ShippingDetails());
            
            // Assert
            result.ShouldBeDefaultView();
        }

        [Test]
        public void Can_Check_Out_And_Submit_Order()
        {
            var mockOrderSubmitter = new Mock<IOrderSubmitter>();
            
            // Arrange: Given a user has a non-empty cart & no validation errors
            var cart = new Cart();
            cart.AddItem(new Product(), 1);
            var shippingDetails = new ShippingDetails();
            
            // Act: When the user tries to check out
            var cartController = new CartController(null, mockOrderSubmitter.Object);
            var result = cartController.CheckOut(cart, shippingDetails);
            
            // Assert: Order goes to the order submitter & user sees "Completed" view
            mockOrderSubmitter.Verify(x => x.SubmitOrder(cart, shippingDetails));
            result.ShouldBeView("Completed");
        }

        [Test]
        public void After_Checking_Out_Cart_Is_Emptied()
        {
            // Arrange/act: Given a valid order submission
            var cart = new Cart();
            cart.AddItem(new Product(), 1);
            new CartController(null, new Mock<IOrderSubmitter>().Object).CheckOut(cart, new ShippingDetails());

            // Assert: The cart is emptied
            cart.Lines.Count.ShouldEqual(0);
        }
    }
}