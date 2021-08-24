using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoojaShop.Core.Contracts;
using PoojaShop.Core.Models;
using PoojaShop.Core.ViewModels;
using PoojaShop.WebUI.Controllers;
using System.Linq;
using System.Web.Mvc;

namespace PoojaShop.WebUI.Tests.Controllers
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void IndexPageDoesReturnProducts()
        {
            IRepository<Product> productContext = new Mocks.MockContext<Product>();
            IRepository<ProductCategory> productCategoryContext = new Mocks.MockContext<ProductCategory>();
            productContext.Insert(new Product());
            
            // Arrange
            HomeController controller = new HomeController(productContext, productCategoryContext);

            // Act
            ViewResult result = controller.Index() as ViewResult;
            var viewModel = (ProductListViewModel)result.ViewData.Model;

            // Assert
            Assert.AreEqual(1,viewModel.Products.Count());
        }

        [TestMethod]
        public void About()
        {
            //// Arrange
            //HomeController controller = new HomeController();

            //// Act
            //ViewResult result = controller.About() as ViewResult;

            //// Assert
            //Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            //HomeController controller = new HomeController();

            //// Act
            //ViewResult result = controller.Contact() as ViewResult;

            //// Assert
            //Assert.IsNotNull(result);
        }
    }
}
