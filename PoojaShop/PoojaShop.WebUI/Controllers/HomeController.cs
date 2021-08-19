using PoojaShop.Core.Contracts;
using PoojaShop.Core.Models;
using PoojaShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PoojaShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;

        //In order to inject the dependencies we need to use DI Container so we nweed to install Unity.MVC and Unity.Container
        //We need to register the repositories in UnityConfig.cs file in App_Start folder under RegisterTypes method
        public HomeController(IRepository<Product> product, IRepository<ProductCategory> productCategory)
        {
            context = product;
            productCategories = productCategory;
        }
        public ActionResult Index(String Category = null)
        {
            List<Product> products;
            List<ProductCategory> categories = productCategories.Collection().ToList();
            if (Category == null)
            {
                products = context.Collection().ToList();

            }
            else
            {
                products = context.Collection().Where(x => x.Category == Category).ToList();
            }

            ProductListViewModel model = new ProductListViewModel();
            model.Products = products;
            model.ProductCategories = categories;
            return View(model);
        }

        public ActionResult Details(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}