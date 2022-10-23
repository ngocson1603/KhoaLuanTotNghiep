using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Khoaluan;
using Khoaluan.Models;
using Khoaluan.OtpModels;
using PagedList.Core;
using System.Drawing.Printing;

namespace Khoaluan.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var x = _unitOfWork.ProductRepository.getallProductwithCategory();
            return View(x);
        }
        public IActionResult Homepage()
        {
            var bestSeller = _unitOfWork.ProductRepository.GetAll().Take(10).ToList();
            var FreeGame = _unitOfWork.ProductRepository.GetAll().Where(t => t.price == 0).ToList();
            var PopularGame = _unitOfWork.ProductRepository.getallProductwithCategory().Where(t => (t.DevName.Equals("EA")) || (t.DevName.Equals("Rockstar Games"))).ToList();
            var RencentlyReleased = _unitOfWork.ProductRepository.getallProductwithCategory().OrderByDescending(t => t.ReleaseDate).ToList();
            HomePageViewModel homepage = new HomePageViewModel()
            {
                bestSeller = bestSeller,
                FreeGames = FreeGame,
                PopularGame = PopularGame,
                RecentlyRealeased = RencentlyReleased
            };
            return View(homepage);
        }
        [Route("/Product/HomePage/{id}.html", Name = ("ProductDetails"))]
        public IActionResult Detail(int id)
        {
            var x=_unitOfWork.ProductRepository.getallProductwithCategory().Where(t=>t.Id==id).FirstOrDefault();
            var relateGame = _unitOfWork.ProductRepository.getallProductwithCategory().Take(6).ToList();
            var popularGame = _unitOfWork.ProductRepository.getallProductwithCategory().Where(t => t.DevName.Equals("Rockstar Games")).ToList();
            var cate = _unitOfWork.CategoryRepository.GetAll().OrderBy(i => i.Id).Take(5).ToList();
            var catesecond = _unitOfWork.CategoryRepository.GetAll().OrderBy(i => i.Id).Skip(5).ToList();
            DetailPage dtp = new DetailPage()
            {
                productDetail = x,
                relateGame=relateGame,
                popularGame=popularGame,
                cate = cate,
                catesecond = catesecond,
            };
            return View(dtp);
        }
        public static string id1;
        [Route("/Product/HomePage/Cate/{id}.html", Name = ("ProductCate"))]
        public IActionResult StoreCatalogAlt(string id, int? page)
        {
            try
            {
                ViewBag.id1 = id.Trim();
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 5;
                var pro = _unitOfWork.ProductRepository.getCate().Where(t => t.CatID.Equals(id)).ToList();
                var pl = pro.AsQueryable().ToPagedList(pageNumber, pageSize);
                var plr = pl.ToList();
                DetailCate pwc = new DetailCate()
                {
                    productwithCate = plr,
                };
                ViewBag.CurrentPage = pageNumber;
                return View(pwc);
            }
            catch
            {
                return RedirectToAction("HomePage", "Product");
            }
        }
    }
}
