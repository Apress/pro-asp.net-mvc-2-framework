using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace jQueryStockQuotesExample
{
    public class JsonpResult : ActionResult
    {
        private object Data { get; set; }
        public JsonpResult(object data) {
            Data = data;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Write(string.Format("{0}({1});",
                HttpUtility.HtmlEncode(context.HttpContext.Request["callback"]),   // Callback method name
                new JavaScriptSerializer().Serialize(Data)                         // Data formatted as JSON
            ));
        }
    }

}