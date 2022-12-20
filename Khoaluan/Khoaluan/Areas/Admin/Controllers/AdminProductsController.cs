using AspNetCoreHero.ToastNotification.Abstractions;
using Khoaluan.Areas.Admin.Models;
using Khoaluan.Enums;
using Khoaluan.Helpper;
using Khoaluan.Models;
using Khoaluan.OtpModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminProductsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public INotyfService _notyfService { get; }
        public AdminProductsController(IUnitOfWork unitOfWork, INotyfService notyfService)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
        }
        public int ProductID
        {
            get
            {
                int? gh = int.Parse(HttpContext.Session.GetString("ProductID"));
                if (gh == null)
                {
                    gh = 0;
                }
                return (int)gh;
            }
        }
        [Authorize(Roles = "Admin")]
        // GET: AdminProductsController
        public IActionResult Index()
        {
            var ls = _unitOfWork.ProductRepository.GetAll().Where(t => t.Status == (int)productType.accept || t.Status == (int)productType.release).ToList();

            return View(ls);
        }

        // GET: AdminProductsController/Details/5
        [Authorize(Roles = "Admin,Dev")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ls = _unitOfWork.ProductRepository.GetById((int)id);
            var item = _unitOfWork.ItemRepository.GetAll().Where(t=>t.ProductId == id).ToList();
            if (ls == null)
            {
                return NotFound();
            }
            HttpContext.Session.SetString("ProductID", id.ToString());
            AdminProduct pwc = new AdminProduct()
            {
                product = ls,
                item = item
            };
            List<string> cate = new List<string>();
            var product1 = _unitOfWork.ProductRepository.getallProductwithCategory().Where(t => t.Id == id).FirstOrDefault();
            if(product1 != null)
            {
                cate.AddRange(product1.Categories);
                ViewData["Category"] = cate;
            }
            else
            {
                ViewData["Category"] = "";
            }
            
            return View(pwc);
        }
        public IActionResult CreateGame()
        {
            var data = new List<MultiDropDownListViewModel>();
            foreach (var item in _unitOfWork.CategoryRepository.GetAll())
            {
                data.Add(new MultiDropDownListViewModel { Id = item.Id, Name = item.Name });
            }
            MultiDropDownListViewModel model = new();
            model.ItemList = data.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            ViewData["Cate"] = model;
            return View(model);
        }

        [HttpPost]
        public IActionResult PostSelectedValues(PostSelectedViewModel model)
        {
            List<int> lst = model.SelectedIds.ToList();
            lst.Remove(2);
            return View();
        }

        // GET: AdminProductsController/Create
    }
}
