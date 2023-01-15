using AspNetCoreHero.ToastNotification.Abstractions;
using Khoaluan.Enums;
using Khoaluan.Models;
using Khoaluan.ModelViews;
using Khoaluan.OtpModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        int release = (int)productType.release;
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
        public string NameItem
        {
            get
            {
                var gh = HttpContext.Session.GetString("NameItem");
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
            var ls = _unitOfWork.SaleProductRepository.ProductNotSale().Where(t => t.Price >= firstprice && t.Price <= secondprice && t.Status == release && t.ReleaseDate <= DateTime.Now).ToList();
            if (name != "all")
            {
            ls = _unitOfWork.SaleProductRepository.ProductNotSale().Where(t => t.Price >= firstprice && t.Price <= secondprice && t.ProductName.ToLower().Contains(name.Trim().ToLower()) && t.Status == release && t.ReleaseDate <= DateTime.Now).ToList();
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
            List<SaleModelView> products = new List<SaleModelView>();
            if (name == "all")
            {
                products = _unitOfWork.SaleProductRepository.ProductNotSale().Where(t=>t.Status == release && t.ReleaseDate <= DateTime.Now).ToList();
            }
            else
            {
                products = _unitOfWork.SaleProductRepository.ProductNotSale().Where(t=>t.ProductName.Contains(name,StringComparison.OrdinalIgnoreCase) && t.Status == release && t.ReleaseDate <= DateTime.Now).ToList();
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
            ViewData["Developers"] = new SelectList(_unitOfWork.DeveloperRepository.GetAll(), "Name", "Name");
            ViewData["Categories"] = new SelectList(_unitOfWork.CategoryRepository.GetAll(), "Name", "Name");
            return View(plr);
        }

        [Route("/my-Item/name={name}.html", Name = ("ItemsByName"))]
        public IActionResult ItemsByName(string name, int? page)
        {
            ViewBag.nameSearchItem = name.Trim();
            HttpContext.Session.SetString("NameItem", name);
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            var item = _unitOfWork.ItemRepository.getItemByUser(int.Parse(taikhoanID));
            var product = _unitOfWork.ProductRepository.listProductItem(int.Parse(taikhoanID)).OrderBy(i => i.Id).ToList();
            if (name == "all")
            {
            }
            else
            {
                item = _unitOfWork.ItemRepository.getItemByUser(int.Parse(taikhoanID)).Where(t => t.NameItem.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (item.Count() <= 10)
                ViewBag.maxPage = 1;
            else
            {
                double dMaxPage = Convert.ToDouble(item.Count());
                ViewBag.maxPage = Math.Ceiling(dMaxPage / 10);
            }
            var pl = item.AsQueryable().ToPagedList(pageNumber, pageSize);
            var plr = pl.ToList();
            AdminProduct ad = new AdminProduct()
            {
                itembyID = plr,
                productdev = product
            };
            ViewBag.CurrentPage = pageNumber;
            return View(ad);
        }

        [Route("/my-Item/Product/{name}.html", Name = ("ItemsByNameProduct"))]
        public IActionResult ItemsByNameProduct(string name, int? page)
        {
            ViewBag.nameSearchItem = name.Trim();
            HttpContext.Session.SetString("NameItem", name);
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            var product = _unitOfWork.ProductRepository.listProductItem(int.Parse(taikhoanID)).OrderBy(i => i.Id).ToList();
            var item = _unitOfWork.ItemRepository.getItemByUser(int.Parse(taikhoanID)).Where(t => t.NameItem.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            if (item.Count() <= 10)
                ViewBag.maxPage = 1;
            else
            {
                double dMaxPage = Convert.ToDouble(item.Count());
                ViewBag.maxPage = Math.Ceiling(dMaxPage / 10);
            }
            var pl = item.AsQueryable().ToPagedList(pageNumber, pageSize);
            var plr = pl.ToList();
            AdminProduct ad = new AdminProduct()
            {
                itembyID = plr,
                productdev = product
            };
            ViewBag.CurrentPage = pageNumber;
            return View(ad);
        }
        //[HttpPost]
        //public IActionResult SearchDanhMuc(string devId, string catId, int? page)
        //{
        //    var pageNumber = page == null || page <= 0 ? 1 : page.Value;
        //    var pageSize = 6;
        //    List<Productdetail> products = new List<Productdetail>();
        //    if (devId != null)
        //    {
        //        products = _unitOfWork.ProductRepository.getallProductwithCategory().Where(t => t.DevName == devId).ToList();
        //    }
        //    else if (catId != null)
        //    {
        //        products = _unitOfWork.ProductRepository.getallProductwithCategory().Where(t => t.CatID == catId).ToList();
        //    }
        //    if (products.Count() <= 6)
        //        ViewBag.maxPage = 1;
        //    else
        //    {
        //        double dMaxPage = Convert.ToDouble(products.Count());
        //        ViewBag.maxPage = Math.Ceiling(dMaxPage / 6);
        //    }
        //    var pl = products.AsQueryable().ToPagedList(pageNumber, pageSize);
        //    var plr = pl.ToList();
        //    ViewBag.CurrentPage = pageNumber;
        //    return View(plr);
        //}
        [HttpPost]
        public IActionResult FindProductsCate(string DevId,string CatId)
        {
            var name = HttpContext.Session.GetString("NamePro");
            List<Productdetail1> ls = new List<Productdetail1>();
            if (DevId!= null)
            {
                ls = _unitOfWork.ProductRepository.getallProductwithCategory1().Where(t => t.DevName == DevId && t.Status == release && t.ReleaseDate <= DateTime.Now).ToList();
            }
            else if (CatId != null)
            {
                ls = _unitOfWork.ProductRepository.getallProductwithCategory1().Where(t => t.Categories.Contains(CatId) && t.Status == release && t.ReleaseDate <= DateTime.Now).ToList();
            }
            if (ls == null)
            {
                return PartialView("ListProductsSearchCatDevPartials", null);
            }
            else
            {
                return PartialView("ListProductsSearchCatDevPartials", ls);
            }
        }
    }
}
