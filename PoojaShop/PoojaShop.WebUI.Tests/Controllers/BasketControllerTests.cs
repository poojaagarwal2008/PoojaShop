using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoojaShop.Core.Contracts;
using PoojaShop.Core.Models;
using PoojaShop.Core.ViewModels;
using PoojaShop.Services;
using PoojaShop.WebUI.Controllers;
using PoojaShop.WebUI.Tests.Mocks;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PoojaShop.WebUI.Tests.Controllers
{
    [TestClass]
    public class BasketControllerTests
    {
        [TestMethod]
        public void CanAddBasketItems()
        {
            //Setup
            IRepository<Basket> basketContext = new MockContext<Basket>();
            IRepository<Product> productContext = new MockContext<Product>();
            IRepository<Order> orderContext = new MockContext<Order>();

            var httpContext = new MockHttpContext();
            IBasketService basketService = new BasketService(productContext, basketContext);
            IOrderService orderService = new OrderService(orderContext);
            var basketController = new BasketController(basketService, orderService);
            basketController.ControllerContext = new ControllerContext(httpContext, new System.Web.Routing.RouteData(), basketController);

            //Act
            basketController.AddtoBasket("2");
            //basketService.AddToBasket(httpContext, "1");

            Basket basket = basketContext.Collection().FirstOrDefault();
            //Assert
            Assert.IsNotNull(basket);
            Assert.AreEqual(1, basket.BasketItems.Count);
            Assert.AreEqual("2", basket.BasketItems.ToList().FirstOrDefault().ProductID);

        }
    
        [TestMethod]

        public void CanGetSummaryViewModel()
        {
            //Setup
            IRepository<Basket> basketContext = new MockContext<Basket>();
            IRepository<Product> productContext = new MockContext<Product>();
            IRepository<Order> orderContext = new MockContext<Order>();
            var httpContext = new MockHttpContext();
            Basket basket = new Basket();
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket") { Value = basket.Id });

            productContext.Insert(new Product { Id = "1", Price = 10.00m });
            productContext.Insert(new Product { Id = "2", Price = 5.00m });

            
            basket.BasketItems.Add(new BasketItem { ProductID = "1", Quantity = 2 });
            basket.BasketItems.Add(new BasketItem { ProductID = "2", Quantity = 1 });
            basketContext.Insert(basket);

            IBasketService basketService = new BasketService(productContext, basketContext);
            IOrderService orderService = new OrderService(orderContext);
            BasketController controller = new BasketController(basketService, orderService);
            controller.ControllerContext = new ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            var partialViewResult = controller.BasketSummary() as PartialViewResult;

            var viewModel = (BasketSummaryViewModel)partialViewResult.ViewData.Model;

            // Assert
            Assert.AreEqual(3, viewModel.BasketCount);
            Assert.AreEqual(25.00m, viewModel.BasketTotal);

        }

        [TestMethod]

        public void CanCheckoutAndCreateOrder()
        {
            //Set up
            IRepository<Product> products = new MockContext<Product>();
            products.Insert(new Product { Id = "1", Price = 10.00m});
            products.Insert(new Product { Id = "2", Price = 5.00m });

            IRepository<Basket> basketContext = new MockContext<Basket>();
            Basket basket = new Basket();
            basket.BasketItems.Add(new BasketItem { BasketID = basket.Id, ProductID = "1", Quantity = 2 });
            basket.BasketItems.Add(new BasketItem { BasketID = basket.Id, ProductID = "2", Quantity = 1 });
            basketContext.Insert(basket);
            IBasketService basketService = new BasketService(products, basketContext);

            IRepository<Order> orderContext = new MockContext<Order>();
            IOrderService orderService = new OrderService(orderContext);

            var controller = new BasketController(basketService, orderService);

            var httpContext = new MockHttpContext();
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket")
            {
                Value = basket.Id
            }) ;
            controller.ControllerContext = new ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            //Act
            Order order = new Order();
            controller.Checkout(order);

            //Assert
            Assert.AreEqual(2, order.OrderItems.Count);
            Assert.AreEqual(0, basket.BasketItems.Count);

        }
            
            
    }

}
