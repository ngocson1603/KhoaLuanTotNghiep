using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DuAnGame.Controllers
{
    public class StoreProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult ProductStore()
        {
            return View();
        }
        public ActionResult StoreCatalog()
        {
            return View();
        }
        public ActionResult StoreCatalogAlt()
        {
            return View();
        }
        public ActionResult Galery()
        {
            return View();
        }
    }
}
