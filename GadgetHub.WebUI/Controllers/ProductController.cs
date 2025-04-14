using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;
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
        public int PageSize = 3;

        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = myrepository.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    // TotalItems = category == null ? myrepository.Products.Count() : myrepository.Products.Count(p => p.category == category)
                    TotalItems = category == null ?
                    myrepository.Products.Count() :
                    myrepository.Products.Where(e => e.Category == category).Count()
                },
                CurrentCategory = category
            };

            return View(model);
        }

        public ViewResult Search(string query, string category, int page = 1)
        {
            var products = myrepository.Products
                .Where(p => (p.Name.Contains(query) || p.Description.Contains(query)) && (category == null || p.Category == category))
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            ProductsListViewModel model = new ProductsListViewModel
            {
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = myrepository.Products.Count(p => (p.Name.Contains(query) || p.Description.Contains(query)) && (category == null || p.Category == category))
                },
                CurrentCategory = category
            };

            return View(model);
        }

        public FileContentResult GetImage(int productId)
        {
            Product prod = myrepository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

    }
}
   