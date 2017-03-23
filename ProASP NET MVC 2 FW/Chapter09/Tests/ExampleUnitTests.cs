using System.Web;
using System.Web.Mvc;
using NUnit.Framework;
using UnitTestingControllers.Controllers;

namespace Tests
{
    [TestFixture]
    public class ExampleUnitTests
    {
        [Test]
        public void Homepage_Recognizes_New_Visitor_And_Sets_Cookie()
        {
            // Arrange
            var controller = new SimpleController();
            var mocks = new ContextMocks(controller); // Sets up complete mock context

            // Act
            ViewResult result = controller.Homepage();

            // Assert
            Assert.IsEmpty(result.ViewName);
            Assert.IsTrue((bool)result.ViewData["IsFirstVisit"]);
            Assert.AreEqual(1, controller.Response.Cookies.Count);
            Assert.AreEqual(bool.TrueString,
            controller.Response.Cookies["HasVisitedBefore"].Value);
        }

        [Test]
        public void Homepage_Recognizes_Previous_Visitor()
        {
            // Arrange
            var controller = new SimpleController();
            var mocks = new ContextMocks(controller);
            controller.Request.Cookies.Add(new HttpCookie("HasVisitedBefore", bool.TrueString));

            // Act
            ViewResult result = controller.Homepage();
            
            // Assert (this time, demonstrating NUnit's alternative "constraint" syntax)
            Assert.That(result.ViewName, Is.EqualTo("HomePage") | Is.Empty);
            Assert.That((bool)result.ViewData["IsFirstVisit"], Is.False);
        }

        [Test]
        public void Homepage_Recognizes_New_Visitor_And_Sets_Cookie_UsingVirtualPropertiesToSimplifyMocking()
        {
            // Arrange
            var controller = new Moq.Mock<SimpleController> { CallBase = true };
            controller.Setup(x => x.IncomingHasVisitedBeforeCookie).Returns((HttpCookie)null);
            controller.SetupProperty(x => x.OutgoingHasVisitedBeforeCookie);

            // Act
            ViewResult result = controller.Object.Homepage();

            // Assert
            Assert.IsEmpty(result.ViewName);
            Assert.IsTrue((bool)result.ViewData["IsFirstVisit"]);
            Assert.AreEqual("True", controller.Object.OutgoingHasVisitedBeforeCookie.Value);
        }
    }
}
