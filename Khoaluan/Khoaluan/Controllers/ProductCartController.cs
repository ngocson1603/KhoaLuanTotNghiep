using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DuAnGame.Controllers
{
    public class ProductCartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Cart()
        {
            return View();
        }
    }
}
