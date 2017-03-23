using System.Collections.Specialized;
using System.Web;

namespace Tests
{
    public class TestHttpContext : HttpContextBase
    {
        TestHttpRequest testRequest;
        TestHttpResponse testResponse;
        public override HttpRequestBase Request { get { return testRequest; } }
        public override HttpResponseBase Response { get { return testResponse; } }

        public TestHttpContext(string url)
        {
            testRequest = new TestHttpRequest()
            {
                _AppRelativeCurrentExecutionFilePath = url
            };
            testResponse = new TestHttpResponse();
        }

        class TestHttpRequest : HttpRequestBase
        {
            public string _AppRelativeCurrentExecutionFilePath { get; set; }
            public override string AppRelativeCurrentExecutionFilePath
            {
                get { return _AppRelativeCurrentExecutionFilePath; }
            }
            public override string ApplicationPath { get { return null; } }
            public override string PathInfo { get { return null; } }
            public override NameValueCollection ServerVariables
            {
                get { return null; }
            }
        }

        class TestHttpResponse : HttpResponseBase
        {
            public override string ApplyAppPathModifier(string x) { return x; }
        }
    }
}