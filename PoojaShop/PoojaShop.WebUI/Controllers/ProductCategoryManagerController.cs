using PoojaShop.Core.Contracts;
using PoojaShop.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PoojaShop.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductCategoryManagerController : Controller
    {
        IRepository<ProductCategory> context;

        //Dependency Injection
        //We need to register the repositories in UnityConfig.cs file in App_Start folder under RegisterTypes method
        public ProductCategoryManagerController(IRepository<ProductCategory> productCategory)
        {
            context = productCategory;
        }


        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = context.Collection().ToList();
            return View(productCategories);
        }

        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            else
            {
                context.Insert(productCategory);
                context.commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            ProductCategory productCategoryToUpdate = context.Find(Id);
            if (productCategoryToUpdate == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategoryToUpdate);
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, string Id)
        {
            ProductCategory productCategoryToUpdate = context.Find(Id);
            if (productCategoryToUpdate == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategory);

                }
                else
                {
                    productCategoryToUpdate.Category = productCategory.Category;
                    context.commit();
                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult Delete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategoryToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")] //This is done because we want to perform actual delete in this function and this is called when user confirms for deletion
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(productCategoryToDelete.Id);
                context.commit();
                return RedirectToAction("Index");
            }
        }
    }
}