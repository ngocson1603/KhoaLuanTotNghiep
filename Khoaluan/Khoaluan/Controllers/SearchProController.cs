using AspNetCoreHero.ToastNotification.Abstractions;
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
        public string key
        {
            get
            {
                var gh = HttpContext.Session.GetString("key");
                if (gh == null)
                {
                    gh = "";
                }
                return gh;
            }
        }
        public void FindProducts(string keyword)
        {
            keyword ??= key;
            HttpContext.Session.SetString("key", keyword.Trim());
        }

        public IActionResult ListSearch()
        {
            var key = HttpContext.Session.GetString("key");
            var ls1 = _unitOfWork.ProductRepository.getallProductwithCategory().Where(t=>t.Name.ToLower().Contains(key.Trim().ToLower())).ToList();
            return View(ls1);
#pragma warning disable CS0162 // Unreachable code detected
            HttpContext.Session.Remove("key");
#pragma warning restore CS0162 // Unreachable code detected
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
