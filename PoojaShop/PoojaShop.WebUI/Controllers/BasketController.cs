using PoojaShop.Core.Contracts;
using PoojaShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoojaShop.WebUI.Controllers
{
    public class BasketController : Controller
    {
        IBasketService basketService;
        IOrderService orderService;

        public BasketController(IBasketService BasketService, IOrderService OrderService)
        {
            this.basketService = BasketService;
            this.orderService = OrderService;
        }
        // GET: Basket
        public ActionResult Index()
        {
            var model = basketService.GetBasketItems(this.HttpContext);
            return View(model);
        }

        public ActionResult AddtoBasket(string Id)
        {
            basketService.AddToBasket(this.HttpContext, Id);
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromBasket(string Id)
        {
            basketService.RemoveFromBasket(this.HttpContext, Id);
            return RedirectToAction("Index");
        }

        public PartialViewResult BasketSummary()
        {
            var basketSummary = basketService.BasketSummary(this.HttpContext);
            return PartialView(basketSummary);
        }

        //We just need to add a view for this as this will take us to the view
        //and the other method will process the actions for this method hence, HttpPost attribute is used for it
        public ActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Checkout(Order order)
        {
            var basketItems = basketService.GetBasketItems(this.HttpContext);
            order.OrderStatus = "Order Created";

            //Payment Process

            order.OrderStatus = "Payment Processed";
            orderService.CreateOrder(order, basketItems);
            basketService.ClearBasket(this.HttpContext);

            return RedirectToAction("Thankyou", new { OrderID = order.Id});
        }

        public ActionResult Thankyou(string OrderID)
        {
            ViewBag.OrderID = OrderID;
            return View();
        }
    }
}