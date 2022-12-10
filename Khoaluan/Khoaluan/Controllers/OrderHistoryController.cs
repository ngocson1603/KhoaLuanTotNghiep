using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Controllers
{
    public class OrderHistoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public INotyfService _notyfService { get; }
        public OrderHistoryController(IUnitOfWork unitOfWork, INotyfService notyfService)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("history-order.html", Name = "OrderHistory")]
        public IActionResult OrderHistory()
        {
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            if (taikhoanID != null)
            {
                var lsDonHang = _unitOfWork.OrderRepository.GetAll()
                    .Where(x => x.UserID == int.Parse(taikhoanID))
                    .OrderByDescending(x => x.DatePurchase)
                    .ToList();
                return View(lsDonHang);
            }
            return RedirectToAction("Homepage", "Product");
        }

        [HttpPost]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var taikhoanID = HttpContext.Session.GetString("CustomerId");
                if (string.IsNullOrEmpty(taikhoanID)) return RedirectToAction("Login", "Users");

                var chitietdonhang = _unitOfWork.OrderDetailRepository.getorder((int)id).ToList();
                return PartialView("Details", chitietdonhang);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
