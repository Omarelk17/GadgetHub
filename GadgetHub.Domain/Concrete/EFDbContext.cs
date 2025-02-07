using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Security.Cryptography.X509Certificates;
using GadgetHub.Domain.Entities;
using GadgetHub.Domain.Abstract;

namespace GadgetHub.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Product> Products { get; set;  }
        
        
        }
    }

