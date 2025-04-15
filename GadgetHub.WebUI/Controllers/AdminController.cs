using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;

namespace GadgetHub.WebUI.Controllers
{
    [Authorize]
    public partial class AdminController : Controller
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

        public ActionResult Edit(int id)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ViewResult Create()
        {
            ViewBag.operation = "create";
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                repository.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been created", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View("Edit", product);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image = null)
        {
            if (repository.Products.Any(p => p.ProductID == product.ProductID))
            {
                if (ModelState.IsValid)
                {
                    if (image != null)
                    {
                        product.ImageMimeType = image.ContentType;
                        product.ImageData = new byte[image.ContentLength];
                        image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                    }
                    repository.SaveProduct(product);
                    TempData["message"] = string.Format("{0} has been saved", product.Name);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(product);
                }
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Delete(int productID)
        {
            Product deleteProduct = repository.Products.FirstOrDefault(p => p.ProductID == productID);
            if (deleteProduct != null)
            {
                repository.DeleteProduct(productID);
                TempData["message"] = string.Format("{0} has been deleted", deleteProduct.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ViewResult List()
        {
            return View(repository.Products);
        }

        public FileContentResult GetImage(int productId)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null && product.ImageData != null)
            {
                return File(product.ImageData, product.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}
