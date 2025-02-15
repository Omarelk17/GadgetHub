using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GadgetHub.Domain.Abstract;
using GadgetHub.WebUI.Models;

namespace GadgetHub.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductsRepository myrepository;

        public ProductController(IProductsRepository productsRepository)
        {
            this.myrepository = productsRepository;
        }
        public int PageSize = 4;

        public ViewResult List(string category, int page = 1)
        {
            var products = myrepository.Products
                .Where(p => category == null || p.category == category)
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = products,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? myrepository.Products.Count() : myrepository.Products.Count(p => p.category == category)
                },
                CurrentCategory = category
            };

            return View(model);
        }

        public ViewResult Search(string query, int page = 1)
        {
            var products = myrepository.Products
                .Where(p => p.Name.Contains(query) || p.Description.Contains(query))
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = products,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = myrepository.Products.Count(p => p.Name.Contains(query) || p.Description.Contains(query))
                },
                CurrentCategory = null
            };

            return View("List", model);
        }
    }
}