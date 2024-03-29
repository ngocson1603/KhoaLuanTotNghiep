﻿using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SearchController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public INotyfService _notyfService { get; }
        public SearchController(IUnitOfWork unitOfWork, INotyfService notyfService)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
        }
        [HttpPost]
        public IActionResult FindProduct(string keyword)
        {
            var ls1 = _unitOfWork.ProductRepository.GetAll().ToList(); ;
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListProductsSearchPartial", ls1);
            }
            var ls = _unitOfWork.ProductRepository.GetAll().Where(t=>t.Name.ToLower().Contains(keyword.Trim().ToLower())).ToList(); ;
            
            if (ls == null || ls.Count == 0)
            {
                return PartialView("ListProductsSearchPartial", null);
            }
            else
            {
                return PartialView("ListProductsSearchPartial", ls);
            }
        }

        [HttpPost]
        public IActionResult FindProductDev(string keyword)
        {
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            var ls1 = _unitOfWork.ProductRepository.listProductDev(int.Parse(taikhoanID)).ToList(); ;
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListProductsSearchPartial", ls1);
            }
            var ls = _unitOfWork.ProductRepository.listProductDev(int.Parse(taikhoanID)).Where(t => t.Name.ToLower().Contains(keyword.Trim().ToLower())).ToList(); ;

            if (ls == null || ls.Count == 0)
            {
                return PartialView("ListProductsSearchPartial", null);
            }
            else
            {
                return PartialView("ListProductsSearchPartial", ls);
            }
        }
        [HttpPost]
        public IActionResult FindItem(string keyword)
        {
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            var ls1 = _unitOfWork.ItemRepository.getItem(int.Parse(taikhoanID));
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListItemSearchPartial", ls1);
            }
            var ls = _unitOfWork.ItemRepository.getItem(int.Parse(taikhoanID)).Where(t => t.Name.ToLower().Contains(keyword.Trim().ToLower())).ToList();

            if (ls == null || ls.Count == 0)
            {
                return PartialView("ListItemSearchPartial", null);
            }
            else
            {
                return PartialView("ListItemSearchPartial", ls);
            }
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FindProductSale(string keyword)
        {
            var saleid = HttpContext.Session.GetInt32("SaleId");
            var ls1 = _unitOfWork.SaleProductRepository.ProductIsSale((int)saleid).ToList();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListSaleProductsSearchPartial", ls1);
            }
            var ls = _unitOfWork.SaleProductRepository.ProductIsSale((int)saleid).Where(t => t.ProductName.ToLower().Contains(keyword.Trim().ToLower())).ToList(); ;

            if (ls == null || ls.Count == 0)
            {
                return PartialView("ListSaleProductsSearchPartial", null);
            }
            else
            {
                return PartialView("ListSaleProductsSearchPartial", ls);
            }
        }
        [HttpPost]
        public IActionResult FindDotSale(string keyword)
        {
            var ls1 = _unitOfWork.SaleRepository.GetAll();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListDotSaleProductsSearchPartial", ls1);
            }
            var ls = _unitOfWork.SaleRepository.GetAll().Where(t => t.Name.ToLower().Contains(keyword.Trim().ToLower())).ToList(); ;

            if (ls == null || ls.Count == 0)
            {
                return PartialView("ListDotSaleProductsSearchPartial", null);
            }
            else
            {
                return PartialView("ListDotSaleProductsSearchPartial", ls);
            }
        }
        [HttpPost]
        public IActionResult FindDev(string keyword)
        {
            var ls1 = _unitOfWork.DeveloperRepository.GetAll();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListDevPartial", ls1);
            }
            var ls = _unitOfWork.DeveloperRepository.GetAll().Where(t => t.Name.ToLower().Contains(keyword.Trim().ToLower())).ToList(); ;

            if (ls == null || ls.Count == 0)
            {
                return PartialView("ListDevPartial", null);
            }
            else
            {
                return PartialView("ListDevPartial", ls);
            }
        }
    }
}
