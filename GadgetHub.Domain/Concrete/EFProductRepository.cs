using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;


namespace GadgetHub.Domain.Concrete
{
    public class EFProductRepository : IProductsRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }

        void IProductsRepository.SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry = context.Products.Find(product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Brand = product.Brand;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;

                    //image
                    dbEntry.ImageData = product.ImageData;
                    dbEntry.ImageMimeType = product.ImageMimeType; 
                }
            }

            context.SaveChanges();
        }

        public Product DeleteProduct(int productID)
        {
            Product dbEntry = context.Products.Find(productID);
            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
