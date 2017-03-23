using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private IProductsRepository productsRepository;
        public int PageSize = 4; // Will change this later

        public ProductsController(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public ViewResult List(string category, int page = 1)
        {
            var productsToShow = (category == null)
                    ? productsRepository.Products
                    : productsRepository.Products.Where(x => x.Category == category);

            var viewModel = new ProductsListViewModel {
                Products = productsToShow.Skip((page - 1) * PageSize).Take(PageSize).ToList(),
                PagingInfo = new PagingInfo {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = productsToShow.Count()
                },
                CurrentCategory = category
            };
            return View(viewModel); // Passed to view as ViewData.Model (or simply Model)
        }

        public FileContentResult GetImage(int productId)
        {
            var product = productsRepository.Products.First(x => x.ProductID == productId);
            return File(product.ImageData, product.ImageMimeType);
        }
    }
}
