using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadgetHub.Domain.Entities
{
    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }

    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }

        public void AddItem(Product myProduct, int myquantity)
        {
            CartLine line = lineCollection
                .Where(p => p.Product.ProductID == myProduct.ProductID)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = myProduct,
                    Quantity = myquantity
                });
            }
            else
            {
                line.Quantity += myquantity;
            }
        }

        public void RemoveLine(Product myProduct)
        {
            lineCollection.RemoveAll(l => l.Product.ProductID == myProduct.ProductID);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Product.Price * e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }
    }
}
