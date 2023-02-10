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
using Khoaluan.Enums;

namespace Khoaluan.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IService _service;
        public ProductController(IUnitOfWork unitOfWork,IService service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
        }

        public IActionResult Index()
        {
            TempData.Keep("idpro");
            var x = _unitOfWork.ProductRepository.getallProductwithCategory();
            return View(x);
        }

        public IActionResult Homepage()
        {
            int release = (int)productType.release;
            var bestSeller = _unitOfWork.SaleProductRepository.ProductNotSale().Where(t=>t.Status == release && t.ReleaseDate <= DateTime.Now).Take(10).ToList();
            var FreeGame = _unitOfWork.ProductRepository.GetAll().Where(t => t.Price == 0 && t.Status == release && t.ReleaseDate <= DateTime.Now).ToList();
            var PopularGame = _unitOfWork.SaleProductRepository.ProductNotSale().Where(t => t.Status == release && t.ReleaseDate <= DateTime.Now).OrderByDescending(t=>t.Price).Take(6).ToList();
            var saleProduct = _unitOfWork.SaleProductRepository.ProductSaleHomePage().Where(t => t.Status == release && t.ReleaseDate <= DateTime.Now).ToList();
            var RencentlyReleased = _unitOfWork.SaleProductRepository.ProductNotSale().Where(t => t.Status == release && t.ReleaseDate <= DateTime.Now).OrderByDescending(t => t.ReleaseDate).ToList();
            var gamesale = _unitOfWork.SaleRepository.GetAll().Where(t => t.EndDate > DateTime.Now).ToList();
            HomePageViewModel homepage = new HomePageViewModel()
            {
                bestSeller = bestSeller,
                FreeGames = FreeGame,
                PopularGame = PopularGame,
                RecentlyRealeased = RencentlyReleased,
                saleProduct = saleProduct
            };
            ViewBag.GameSale = gamesale;
            TempData.Keep("idpro");
            return View(homepage);
        }

        [Route("/Product/HomePage/{id}.html", Name = ("ProductDetails"))]
        public IActionResult Detail(int id)
        {
            int release = (int)productType.release;
            var discount=_unitOfWork.SaleProductRepository.getdiscount(id);
            var x = _unitOfWork.ProductRepository.getallProductwithCategory().Where(t => t.Id == id).FirstOrDefault();          
            if(discount!=-1)
            {
                x.Discount = discount;
            }
            else
            {
                
            }
            var relateGame = _unitOfWork.SaleProductRepository.ProductNotSale().Where(t => t.Status == release && t.ReleaseDate <= DateTime.Now).Take(6).ToList();
            var popularGame = _unitOfWork.SaleProductRepository.ProductNotSale().Where(t => t.DevName.Equals("Rockstar Games") && t.Status == release && t.ReleaseDate <= DateTime.Now).ToList();
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

            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            if(taikhoanID == null)
            {
                ViewBag.GameDangHoanTien = true;
            }
            else
            {
                ViewBag.GameDangHoanTien = _service.ProductService.IsGameCanBuy(int.Parse(taikhoanID), id);
            }
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
                int release = (int)productType.release;
                ViewBag.id1 = id.Trim();
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 6;
                var popular =_unitOfWork.SaleProductRepository.ProductNotSale().Where(t => t.Status == release && t.ReleaseDate <= DateTime.Now).Take(6).ToList();
                var pro = _unitOfWork.ProductRepository.getallProductwithCategory1().Where(t => t.Categories.Contains(id)).ToList();
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
                int release = (int)productType.release;
                ViewBag.id1 = id.Trim();
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 6;
                var popular = _unitOfWork.SaleProductRepository.ProductNotSale().Where(t => t.Status == release && t.ReleaseDate <= DateTime.Now).Take(6).ToList();
                var pro = _unitOfWork.SaleProductRepository.ProductNotSale().Where(t => t.DevName.Equals(id) && t.Status == release && t.ReleaseDate <= DateTime.Now).ToList();
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
                    productwithCateDev = plr,
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
