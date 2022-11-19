using AspNetCoreHero.ToastNotification.Abstractions;
using Khoaluan.Models;
using Khoaluan.OtpModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Controllers
{
    public class SearchProController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public INotyfService _notyfService { get; }
        public SearchProController(IUnitOfWork unitOfWork, INotyfService notyfService)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
        }
        [HttpGet]
        public IActionResult FindProductsByName()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ProductsByName([FromQuery(Name = "name")] string name)
        {
            List<Product> products = new List<Product>();
            if (name == "all" || name == null)
            {
                products = _unitOfWork.ProductRepository.GetAll();
            }
            else
            {
                products = _unitOfWork.ProductRepository.GetProductByName(name.ToLower().Trim());
            }
            return View(products);
        }
    }
}
