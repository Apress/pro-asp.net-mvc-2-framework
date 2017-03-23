﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductsRepository productsRepository;
        
        public NavController(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public ViewResult Menu(string category)
        {
            // Just so we don't have to write this code twice
            Func<string, NavLink> makeLink = categoryName => new NavLink
            {
                Text = categoryName ?? "Home",
                RouteValues = new RouteValueDictionary(new {
                    controller = "Products", action = "List",
                    category = categoryName, page = 1
                }),
                IsSelected = (categoryName == category)
            };

            // Put a Home link at the top
            List<NavLink> navLinks = new List<NavLink>();
            navLinks.Add(makeLink(null));

            // Add a link for each distinct category
            var categories = productsRepository.Products.Select(x => x.Category);
            foreach (string categoryName in categories.Distinct().OrderBy(x => x))
                navLinks.Add(makeLink(categoryName));

            return View(navLinks);
        }
    }
}
