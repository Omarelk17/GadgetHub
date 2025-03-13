using GadgetHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadgetHub.Domain.Abstract
{
    public interface IOrderProcessor // Change to public
    {
        void ProcessOrder(Cart cart, Entities.ShippingDetails shippingDetails);
    }
}

