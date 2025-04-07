using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GadgetHub.Domain.Concrete;
using GadgetHub.Domain.Entities;

namespace GadgetHub.Domain.Abstract
{

    public interface IProductsRepository
    {
        IEnumerable<Product> Products { get; }
        void SaveProduct(Product product);
        Product DeleteProduct(int productID);
    }
}