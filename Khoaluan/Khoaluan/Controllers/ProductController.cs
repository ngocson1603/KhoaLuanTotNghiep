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
            var detail = _unitOfWork.ProductRepository.getallProductwithCategory().Where(t=>t.Id==id).FirstOrDefault();
            var cate = _unitOfWork.CategoryRepository.GetAll().OrderBy(i => i.Id).Take(5).ToList();
            var catesecond = _unitOfWork.CategoryRepository.GetAll().OrderBy(i => i.Id).Skip(5).ToList();
            HomePageViewModel homepage = new HomePageViewModel()
            {
                cate = cate,
                catesecond = catesecond,
                detail = detail,
            };
            return View(homepage);
        }
    }
}
