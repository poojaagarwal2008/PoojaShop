using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoojaShop.WebUI.Controllers
{
    //Tpo only allow users with Admin role to access the Admin views
    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}