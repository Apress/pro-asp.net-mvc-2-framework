using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AlternativeViewEngines.Models;
using MvcContrib.ViewEngines;
using MvcContrib.ViewFactories;
using NHaml.Web.Mvc;
using Spark;
using Spark.Web.Mvc;

namespace AlternativeViewEngines.Controllers
{
    public class ViewEnginesController : Controller
    {
        // Note: For convenience, each of these action methods constructs its own view engine instance.
        // However, for performance, *don't* do this - it's better to construct only one view engine 
        // instance when the app first starts up. See Global.asax.cs to see how XSLTViewEngine is 
        // constructed only once and then stored in ViewEngines.Engines.

        public ViewResult NVelocity()
        {
            ViewResult result = CreateViewResult();
            result.ViewEngineCollection = new ViewEngineCollection { new NVelocityViewEngine() };
            return result;
        }

        public ViewResult Brail()
        {
            ViewResult result = CreateViewResult();
            result.ViewEngineCollection = new ViewEngineCollection { new BrailViewFactory() };
            return result;
        }

        public ViewResult NHaml()
        {
            ViewResult result = CreateViewResult();
            result.ViewEngineCollection = new ViewEngineCollection { new NHamlMvcViewEngine() };
            return result;
        }

        public ViewResult Spark()
        {
            ViewResult result = CreateViewResult();
            result.ViewEngineCollection = new ViewEngineCollection { new SparkViewFactory(
                new SparkSettings()
                    .AddNamespace("System.Collections.Generic")
                    .AddNamespace("System.Web.Mvc.Html")
                    .AddNamespace("AlternativeViewEngines.Models")
            )};
            return result;
        }

        public ViewResult WebForm()
        {
            ViewResult result = CreateViewResult();
            result.ViewEngineCollection = new ViewEngineCollection { new WebFormViewEngine() };
            return result;
        }

        private ViewResult CreateViewResult()
        {
            ViewData["message"] = "Hello, world!";
            ViewData.Model = new List<Mountain> {
                new Mountain {
                    Name = "Everest",
                    Height = 8848,
                    DateDiscovered = new DateTime(1732, 10, 3)
                },
                new Mountain {
                    Name = "Kilimanjaro",
                    Height = 5895,
                    DateDiscovered = new DateTime(1995, 3, 1)
                },
                new Mountain {
                    Name = "Snowdon",
                    Height = 1085,
                    DateDiscovered = new DateTime(1661, 4, 15)
                },
            };
            return View("Index");
        }
    }
}