using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Tests
{
    public class ContextMocks
    {
        public Moq.Mock<HttpContextBase> HttpContext { get; private set; }
        public Moq.Mock<HttpRequestBase> Request { get; private set; }
        public Moq.Mock<HttpResponseBase> Response { get; private set; }
        public RouteData RouteData { get; private set; }
        public ContextMocks(Controller onController)
        {
            // Define all the common context objects, plus relationships between them
            HttpContext = new Moq.Mock<HttpContextBase>();
            Request = new Moq.Mock<HttpRequestBase>();
            Response = new Moq.Mock<HttpResponseBase>();
            HttpContext.Setup(x => x.Request).Returns(Request.Object);
            HttpContext.Setup(x => x.Response).Returns(Response.Object);
            HttpContext.Setup(x => x.Session).Returns(new FakeSessionState());
            Request.Setup(x => x.Cookies).Returns(new HttpCookieCollection());
            Response.Setup(x => x.Cookies).Returns(new HttpCookieCollection());
            Request.Setup(x => x.QueryString).Returns(new NameValueCollection());
            Request.Setup(x => x.Form).Returns(new NameValueCollection());

            // Apply the mock context to the supplied controller instance
            RequestContext rc = new RequestContext(HttpContext.Object, new RouteData());
            onController.ControllerContext = new ControllerContext(rc, onController);
        }

        // Use a fake HttpSessionStateBase, because it's hard to mock it with Moq
        private class FakeSessionState : HttpSessionStateBase
        {
            Dictionary<string, object> items = new Dictionary<string, object>();
            public override object this[string name]
            {
                get { return items.ContainsKey(name) ? items[name] : null; }
                set { items[name] = value; }
            }
        }
    }
}
