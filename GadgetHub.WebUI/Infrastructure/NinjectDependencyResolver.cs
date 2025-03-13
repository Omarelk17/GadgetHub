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
using GadgetHub.Domain.Concrete;
using System.Configuration;
using static GadgetHub.Domain.Concrete.EmailOrderProcessor; // Add this using directive

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
            mykernel.Bind<IProductsRepository>().To<EFProductRepository>();
            EmailSettings emailSettings = new EmailSettings
            {
              WriteAsFile = bool.Parse
              (ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };
            mykernel.Bind<IOrderProcessor>()
                .To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);
        }
    }
}
