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
            //var popularGame = _unitOfWork.ProductRepository
            //    .getallProductwithCategory().Where(t => t.Categories
            //    .Contains("Action")).ToList();
            var topgame = _unitOfWork.ProductRepository.getallProductwithCategory().Where(t => (t.DevName.Equals("EA")) || (t.DevName.Equals("Rockstar Games"))).ToList();
            var onegame = _unitOfWork.ProductRepository.GetAll().Where(t => t.Id == 1).ToList();
            var onegame1 = _unitOfWork.ProductRepository.GetAll().Where(t => t.Id == 2).ToList();
            var fourgame = _unitOfWork.ProductRepository.getallProductwithCategory().Where(t => t.DevName.Equals("EA")).ToList();
            HomePageViewModel homepage = new HomePageViewModel()
            {
                bestSeller = bestSeller,
                FreeGames = FreeGame,
                //PopularGame = popularGame,
                topgame = topgame,
                onegame = onegame,
                onegame1 = onegame1,
                fourgame = fourgame
            };
            return View(homepage);
        }
        [Route("/Product/HomePage/{id}.html", Name = ("ProductDetails"))]
        public IActionResult Detail(int id)
        {
            var x=_unitOfWork.ProductRepository.getallProductwithCategory().Where(t=>t.Id==id).FirstOrDefault();
            return View(x);
        }
    }
}
