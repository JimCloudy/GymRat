using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkingOut.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.HomeTab = "active";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.AboutTab = "active";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.ContactTab = "active";
            return View();
        }
    }
}