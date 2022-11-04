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
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Khoaluan.Extension;

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
            TempData.Keep("idpro");
            var x = _unitOfWork.ProductRepository.getallProductwithCategory();
            return View(x);
        }

        public IActionResult Homepage()
        {
            var bestSeller = _unitOfWork.ProductRepository.GetAll().Take(10).ToList();
            var FreeGame = _unitOfWork.ProductRepository.GetAll().Where(t => t.Price == 0).ToList();
            var PopularGame = _unitOfWork.ProductRepository.getallProductwithCategory().Where(t => (t.DevName.Equals("EA")) || (t.DevName.Equals("Rockstar Games"))).ToList();
            var RencentlyReleased = _unitOfWork.ProductRepository.getallProductwithCategory().OrderByDescending(t => t.ReleaseDate).ToList();
            HomePageViewModel homepage = new HomePageViewModel()
            {
                bestSeller = bestSeller,
                FreeGames = FreeGame,
                PopularGame = PopularGame,
                RecentlyRealeased = RencentlyReleased
            };
            TempData.Keep("idpro");
            return View(homepage);
        }

        [Route("/Product/HomePage/{id}.html", Name = ("ProductDetails"))]
        public IActionResult Detail(int id)
        {
            var x = _unitOfWork.ProductRepository.getallProductwithCategory().Where(t => t.Id == id).FirstOrDefault();
            var relateGame = _unitOfWork.ProductRepository.getallProductwithCategory().Take(6).ToList();
            var popularGame = _unitOfWork.ProductRepository.getallProductwithCategory().Where(t => t.DevName.Equals("Rockstar Games")).ToList();
            var cate = _unitOfWork.CategoryRepository.GetAll().OrderBy(i => i.Id).ToList();
            var cart = HttpContext.Session.Get<List<Cart>>("_GioHang");
            GetLibrary();
            DetailPage dtp = new DetailPage()
            {
                productDetail = x,
                relateGame = relateGame,
                popularGame = popularGame,
                cate = cate,
            };
            ViewBag.GioHang = cart;
            return View(dtp);
        }
        public void GetLibrary()
        {
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            ViewBag.GetId = taikhoanID;
            if (taikhoanID != null)
            {
                var khachhang = _unitOfWork.UserRepository.GetAll().SingleOrDefault(x => x.Id == Convert.ToInt32(taikhoanID));
                if (khachhang != null)
                {
                    var proLib = _unitOfWork.LibraryRepository.getLibrary(khachhang.Id);
                    ViewBag.DonHang = proLib;
                }
            }
        }
        public static string id1;
        public static int maxPage;

        [Route("/Product/HomePage/Cate/{id}.html", Name = ("ProductCate"))]
        public IActionResult StoreCatalogAlt(string id, int? page)
        {
            try
            {
                ViewBag.id1 = id.Trim();
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 6;
                var popular = _unitOfWork.ProductRepository.getallProductwithCategory().Take(6).ToList();
                var pro = _unitOfWork.ProductRepository.getallProductwithCategory().Where(t => t.Categories.Contains(id)).ToList();
                ViewBag.Category = id;
                if (pro.Count() <= 6)
                    ViewBag.maxPage = 1;
                else
                {
                    double dMaxPage = Convert.ToDouble(pro.Count());
                    ViewBag.maxPage = Math.Ceiling(dMaxPage / 6);
                }

                var pl = pro.AsQueryable().ToPagedList(pageNumber, pageSize);
                var plr = pl.ToList();
                DetailCate pwc = new DetailCate()
                {
                    productwithCate = plr,
                    PopularGame1 = popular
                };
                TempData.Keep("idpro");
                ViewBag.CurrentPage = pageNumber;
                return View(pwc);
            }
            catch
            {
                return RedirectToAction("HomePage", "Product");
            }
        }

        [Route("/Product/HomePage/Dev/{id}.html", Name = ("ProductDev"))]
        public IActionResult StoreDevAlt(string id, int? page)
        {
            try
            {
                ViewBag.id1 = id.Trim();
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 6;
                var popular = _unitOfWork.ProductRepository.getallProductwithCategory().Take(6).ToList();
                var pro = _unitOfWork.ProductRepository.getallProductwithCategory().Where(t => t.DevName.Equals(id)).ToList();
                if (pro.Count() <= 6)
                    ViewBag.maxPage = 1;
                else
                {
                    double dMaxPage = Convert.ToDouble(pro.Count());
                    ViewBag.maxPage = Math.Ceiling(dMaxPage / 6);
                }

                var pl = pro.AsQueryable().ToPagedList(pageNumber, pageSize);
                var plr = pl.ToList();
                DetailCate pwc = new DetailCate()
                {
                    productwithCate = plr,
                    PopularGame1=popular
                };
                TempData.Keep("idpro");
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
