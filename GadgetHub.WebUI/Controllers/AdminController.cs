using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;

namespace GadgetHub.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IProductsRepository repository;
        public AdminController(IProductsRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult Index()
        {
            return View(repository.Products);
        }

        public ViewResult ReadInfo(int id)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == id);
            return View(product);
        }
    }
}
