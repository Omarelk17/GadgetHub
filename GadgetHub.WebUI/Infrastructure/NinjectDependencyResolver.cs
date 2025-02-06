using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;
using Moq;


namespace GadgetHub.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel mykernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        { 
            mykernel = kernelParam;
            AddBinding();
        }
        public object GetService(Type myserviceType)
        {
            return mykernel.GetService(myserviceType);
        }
        public IEnumerable<object> GetServices(Type myserviceType)
        {
            return mykernel.GetAll(myserviceType);
        }
        public void AddBinding()
        {
            Mock<IProductsRepository> myMock = new Mock<IProductsRepository>();
            myMock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product {Name = "Laptop", Price = 250, Description = "i9 HP Laptop" },
                new Product {Name = "iPhone", Price = 550, Description = "This is an iPhone 14pro" },
                new Product {Name = "Samsung", Price = 350, Description = "This is a samsung zflip" }
                });
           
            mykernel.Bind<IProductsRepository>().ToConstant(myMock.Object);
        }
     }
  }
