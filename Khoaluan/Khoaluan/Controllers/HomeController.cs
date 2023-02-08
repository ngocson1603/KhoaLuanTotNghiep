using Khoaluan.Enums;
using Khoaluan.Models;
using Khoaluan.OtpModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            int release = (int)productType.release;
            var blog = _unitOfWork.BlogRepository.GetAll().Where(t=>t.Published == true).ToList();
            var product = _unitOfWork.SaleProductRepository.ProductNotSale().Where(t => t.Status == release && t.ReleaseDate <= DateTime.Now).OrderByDescending(t => t.Price).Take(4).ToList();
            ListBlog blogls = new ListBlog()
            {
                listBlogs = blog,
                listProducts = product
            };
            return View(blogls);
        }
        
        public ActionResult Support()
        {
            return View();
        }
        [Route("Privacy.html", Name = "Privacy")]
        public ActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
