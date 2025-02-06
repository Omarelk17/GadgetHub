using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
 

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
            //we need to put bindings here
        }
     }
  }
