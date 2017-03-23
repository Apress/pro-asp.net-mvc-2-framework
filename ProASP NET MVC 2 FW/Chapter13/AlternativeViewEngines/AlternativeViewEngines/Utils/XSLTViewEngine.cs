using System.Web.Mvc;

namespace AlternativeViewEngines.Utils
{
    public class XSLTViewEngine : VirtualPathProviderViewEngine
    {
        public XSLTViewEngine()
        {
            ViewLocationFormats = PartialViewLocationFormats = new[] {
                "~/Views/{1}/{0}.xslt",
                "~/Views/Shared/{0}.xslt",
            };

            AreaViewLocationFormats = AreaPartialViewLocationFormats = new[] {
                "~/Areas/{2}/Views/{1}/{0}.xslt",
                "~/Areas/{2}/Views/Shared/{0}.xslt",
            };
        }

        protected override IView CreateView(ControllerContext controllerContext,
                                            string viewPath, string masterPath)
        {
            // This view engine doesn't have any concept of master pages,
            // so it can ignore any requests to use a master page
            return new XSLTView(controllerContext, viewPath);
        }

        protected override IView CreatePartialView(ControllerContext controllerContext,
                                                   string partialPath)
        {
            // This view engine doesn't need to distinguish between 
            // partial views and regular views, so it simply calls
            // the regular CreateView() method
            return CreateView(controllerContext, partialPath, null);
        }
    }
}