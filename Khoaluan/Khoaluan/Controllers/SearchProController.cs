using AspNetCoreHero.ToastNotification.Abstractions;
using Khoaluan.Models;
using Khoaluan.OtpModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
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

        public string NamePro
        {
            get
            {
                var gh = HttpContext.Session.GetString("NamePro");
                if (gh == null)
                {
                    gh = "";
                }
                return gh;
            }
        }

        

        [HttpGet]
        public IActionResult FindProductsByName()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FindProducts(int firstprice, int secondprice)
        {
            var name = HttpContext.Session.GetString("NamePro");
            var ls = _unitOfWork.ProductRepository.GetAll().Where(t => t.Price >= firstprice && t.Price <= secondprice).ToList();
            if (name != "all")
            {
            ls = _unitOfWork.ProductRepository.GetAll().Where(t => t.Price >= firstprice && t.Price <= secondprice && t.Name.ToLower().Contains(name.Trim().ToLower())).ToList();
            }    
            if (ls == null)
            {
                return PartialView("ListProductsSearchPartials", null);
            }
            else
            {
                return PartialView("ListProductsSearchPartials", ls);
            }
        }

        [Route("/SearchPro/ProductsByName/name={name}.html", Name = ("ProductSearch"))]
        public IActionResult ProductsByName(string name, int? page)
        {
            ViewBag.nameSearch = name.Trim();
            HttpContext.Session.SetString("NamePro", name);
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 6;
            List<Product> products = new List<Product>();
            if (name == "all")
            {
                products = _unitOfWork.ProductRepository.GetAll();
            }
            else
            {
                products = _unitOfWork.ProductRepository.GetProductByName(name.ToLower().Trim());
            }
            if (products.Count() <= 6)
                ViewBag.maxPage = 1;
            else
            {
                double dMaxPage = Convert.ToDouble(products.Count());
                ViewBag.maxPage = Math.Ceiling(dMaxPage / 6);
            }
            var pl = products.AsQueryable().ToPagedList(pageNumber, pageSize);
            var plr = pl.ToList();
            ViewBag.CurrentPage = pageNumber;
            return View(plr);
        }
    }
}
