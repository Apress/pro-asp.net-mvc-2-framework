using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductsRepository productsRepository;
        public AdminController(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public ViewResult Index()
        {
            return View(productsRepository.Products.ToList());
        }

        public ViewResult Edit(int productId)
        {
            var product = productsRepository.Products.First(x => x.ProductID == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(int productId, HttpPostedFileBase image)
        {
            Product product = productId == 0
                ? new Product()
                : productsRepository.Products.First(x => x.ProductID == productId);
            TryUpdateModel(product);

            if (ModelState.IsValid)
            {
                if (image != null) {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }

                productsRepository.SaveProduct(product);
                TempData["message"] = product.Name + " has been saved.";
                return RedirectToAction("Index");
            }
            else // Validation error, so redisplay same view
                return View(product);
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        public RedirectToRouteResult Delete(int productId)
        {
            var product = productsRepository.Products.First(x => x.ProductID == productId);
            productsRepository.DeleteProduct(product);
            TempData["message"] = product.Name + " was deleted";
            return RedirectToAction("Index");
        }
    }
}
