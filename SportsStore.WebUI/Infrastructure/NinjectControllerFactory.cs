using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using Moq;

namespace SportsStore.WebUI.Infrastructure {
    // user's factory of the controllers
    public class NinjectControllerFactory : DefaultControllerFactory {
        private IKernel ninjectKernel;
        public NinjectControllerFactory() {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType) {
            // get object of the controller
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }
        private void AddBindings() {
            ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();
        }
    }
}