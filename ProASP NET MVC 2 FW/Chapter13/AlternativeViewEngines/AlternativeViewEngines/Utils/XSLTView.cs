using System;
using System.IO;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace AlternativeViewEngines.Utils
{
    public class XSLTView : IView
    {
        private readonly XslCompiledTransform _template;

        public XSLTView(ControllerContext controllerContext, string viewPath)
        {
            // Load the view template
            _template = new XslCompiledTransform();
            _template.Load(controllerContext.HttpContext.Server.MapPath(viewPath));
        }

        public void Render(ViewContext viewContext, TextWriter writer)
        {
            // Check that the incoming ViewData is legal
            XDocument xmlModel = viewContext.ViewData.Model as XDocument;
            if (xmlModel == null)
                throw new ArgumentException("ViewData.Model must be an XDocument");

            // Run the transformation directly to the output stream
            _template.Transform(xmlModel.CreateReader(), null, writer);
        }
    }
}