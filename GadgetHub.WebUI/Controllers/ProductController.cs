using System;
using System.Collections.Generic;
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


        // GET: Product
        public int PageSize = 2;
        public ViewResult List(int page = 1)
        {
            // Skip(int) - ignores the specified number of items
            //and then returns a sequence starting at the item after 
            // last skipped item (if any)
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = myrepository.Products.OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),

            PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = myrepository.Products.Count()
            }


         };
                return View(model);
        }
    }
}