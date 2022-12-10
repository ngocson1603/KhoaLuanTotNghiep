using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Controllers
{
    public class PageController : Controller
    {
        [Route("coming-soon.html", Name = "comingsoon")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
