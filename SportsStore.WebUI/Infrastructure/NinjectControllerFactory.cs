﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Abstract;
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
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product> {
                new Product { Name = "Football",Price = 25},
                new Product { Name = "Surf board",Price = 179},
                new Product { Name = "Running shoes",Price = 95}
            }.AsQueryable());
            ninjectKernel.Bind<IProductRepository>().ToConstant(mock.Object);
        }
    }
}