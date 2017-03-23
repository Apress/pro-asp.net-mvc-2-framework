using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
using UnitTestingRoutes;
using UnitTestingRoutes.Areas.Admin;

namespace Tests
{
    [TestFixture]
    public class InboundRouteMatching
    {
        [Test]
        public void ForwardSlashGoesToHomeIndexViaTestDouble()
        {
            TestRouteViaTestDouble("~/", new { controller = "Home", action = "Index" });
        }

        [Test]
        public void ForwardSlashGoesToHomeIndexViaMocks()
        {
            TestRouteViaMocks("~/", new { controller = "Home", action = "Index" });
        }

        [Test]
        public void EditProduct50_IsAt_Products_Edit_50_ViaTestDouble()
        {
            string result = GenerateUrlViaTestDouble(
                new { controller = "Products", action = "Edit", id = 50 }
            );
            Assert.AreEqual("/Products/Edit/50", result);
        }

        [Test]
        public void EditProduct50_IsAt_Products_Edit_50_ViaMocks()
        {
            string result = GenerateUrlViaMocks(
                new { controller = "Products", action = "Edit", id = 50 }
            );
            Assert.AreEqual("/Products/Edit/50", result);
        }

        [Test]
        public void AdminAreaStatsExport_IsAt_Admin_Stats_Export()
        {
            string result = GenerateUrlViaMocks(
                new { area = "Admin", controller = "Stats", action = "Export" }
            );
            Assert.AreEqual("/Admin/Stats/Export", result);
        }

        private static RouteData TestRouteViaTestDouble(string url, object expectedValues)
        {
            // Arrange (obtain routing config + set up test context)
            RouteCollection routeConfig = new RouteCollection();
            MvcApplication.RegisterRoutes(routeConfig);
            var mockHttpContext = new TestHttpContext(url);
            // Act (run the routing engine against this HttpContextBase)
            RouteData routeData = routeConfig.GetRouteData(mockHttpContext);
            // Assert
            Assert.IsNotNull(routeData.Route, "No route was matched");
            var expectedDict = new RouteValueDictionary(expectedValues);
            foreach (var expectedVal in expectedDict)
            {
                if (expectedVal.Value == null)
                    Assert.IsNull(routeData.Values[expectedVal.Key]);
                else
                    Assert.AreEqual(expectedVal.Value.ToString(),
                    routeData.Values[expectedVal.Key].ToString());
            }
            return routeData; // ... in case the caller wants to add any other assertions
        }

        private static RouteData TestRouteViaMocks(string url, object expectedValues)
        {
            // Arrange (obtain routing config + set up test context)
            RouteCollection routeConfig = new RouteCollection();
            MvcApplication.RegisterRoutes(routeConfig);
            var mockHttpContext = RoutingMockHelpers.MakeMockHttpContext(url);
            // Act (run the routing engine against this HttpContextBase)
            RouteData routeData = routeConfig.GetRouteData(mockHttpContext.Object);
            // Assert
            Assert.IsNotNull(routeData.Route, "No route was matched");
            var expectedDict = new RouteValueDictionary(expectedValues);
            foreach (var expectedVal in expectedDict)
            {
                if (expectedVal.Value == null)
                    Assert.IsNull(routeData.Values[expectedVal.Key]);
                else
                    Assert.AreEqual(expectedVal.Value.ToString(),
                    routeData.Values[expectedVal.Key].ToString());
            }
            return routeData; // ... in case the caller wants to add any other assertions
        }

        private static string GenerateUrlViaTestDouble(object values)
        {
            // Arrange (get the routing config and test context)
            RouteCollection routeConfig = new RouteCollection();
            RegisterAllAreas(routeConfig);
            MvcApplication.RegisterRoutes(routeConfig);
            var testContext = new TestHttpContext(null);
            RequestContext context = new RequestContext(testContext, new RouteData());
            // Act (generate a URL)
            return UrlHelper.GenerateUrl(null, null, null, /* Explained below */
            new RouteValueDictionary(values), routeConfig, context, true);
        }

        private string GenerateUrlViaMocks(object values)
        {
            // Arrange (get the routing config and test context)
            RouteCollection routeConfig = new RouteCollection();
            RegisterAllAreas(routeConfig);
            MvcApplication.RegisterRoutes(routeConfig);
            var mockContext = RoutingMockHelpers.MakeMockHttpContext(null);
            RequestContext context = new RequestContext(mockContext.Object, new RouteData());
            // Act (generate a URL)
            return UrlHelper.GenerateUrl(null, null, null,
            new RouteValueDictionary(values), routeConfig, context, true);
        }

        private static void RegisterAllAreas(RouteCollection routes)
        {
            var allAreas = new AreaRegistration[] {
                new AdminAreaRegistration()
                // ...etc. (Manually add whichever ones you're using)
            };

            foreach (AreaRegistration area in allAreas) {
                var context = new AreaRegistrationContext(area.AreaName, routes);
                context.Namespaces.Add(area.GetType().Namespace);
                area.RegisterArea(context);
            }
        }
    }
}