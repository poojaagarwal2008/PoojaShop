using PoojaShop.Core.Contracts;
using PoojaShop.Core.Models;
using PoojaShop.DataAccess.SQL;
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
        IRepository<Customer> customerContext;

        public BasketController(IBasketService BasketService, IOrderService OrderService, IRepository<Customer> CustomerContext)
        {
            this.basketService = BasketService;
            this.orderService = OrderService;
            this.customerContext = CustomerContext;
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
        [Authorize] //To make sure that only a logged in user can perform this action else it redirects to the login page
        public ActionResult Checkout()
        {
            Customer customer = customerContext.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);
            if (customer != null)
            {
                Order order = new Order
                {
                    Email = customer.Email,
                    FirstName = customer.FirstName,
                    Surname = customer.LastName,
                    City = customer.City,
                    Street = customer.Street,
                    Zipcode = customer.Zipcode,
                    State = customer.State
                };

                return View(order);
            }
            else
            {
                return RedirectToAction("Error");
            }            
        }

        [HttpPost]
        [Authorize]
        public ActionResult Checkout(Order order)
        {
            var basketItems = basketService.GetBasketItems(this.HttpContext);
            order.OrderStatus = "Order Created";
            order.Email = User.Identity.Name;

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