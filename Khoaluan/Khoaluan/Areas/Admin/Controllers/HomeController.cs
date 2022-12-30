using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Khoaluan;
using AspNetCoreHero.ToastNotification.Abstractions;
using Khoaluan.OtpModels;
using Khoaluan.ModelViews;

namespace DuAnGame.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Dev")]
    [Area("Admin")]
    [Route("admin.html", Name = "AdminIndex")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public INotyfService _notyfService { get; }
        public HomeController(IUnitOfWork unitOfWork, INotyfService notyfService)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
        }
        public IActionResult Index()
        {
            var pro = _unitOfWork.SaleProductRepository.ProductSellInMonth().ToList();
            HomeProduct pwc = new HomeProduct()
            {
                product = pro,
            };
            return View(pwc);
        }
    }
}
